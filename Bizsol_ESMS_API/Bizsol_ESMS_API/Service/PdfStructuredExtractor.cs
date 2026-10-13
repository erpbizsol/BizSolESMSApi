using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig.Content;

namespace Bizsol_ESMS_API.Service;

internal static class PdfStructuredExtractor
{
    private const double RowYTolerance = 5.0;
    private const double DefaultCellGapThreshold = 8.0;

    private static readonly string[] HeaderHints =
    [
        "s/n", "sn", "p/n", "description", "mrp", "dap", "part", "qty", "rate", "amount"
    ];

    public static (Dictionary<string, string> Fields, List<Dictionary<string, string>> Grid) ExtractPage(Page page)
    {
        var words = page.GetWords().ToList();
        var plainText = page.Text ?? string.Empty;

        Dictionary<string, string> fields;
        List<Dictionary<string, string>> grid;

        if (words.Count > 0)
        {
            (fields, grid) = ExtractFromWordPositions(words);
        }
        else
        {
            fields = BuildFieldsFromText(plainText);
            grid = [];
        }

        if (grid.Count == 0 && !string.IsNullOrWhiteSpace(plainText))
        {
            var (textFields, textGrid) = ExtractFromPlainText(plainText);
            foreach (var pair in textFields)
            {
                fields[pair.Key] = pair.Value;
            }

            grid = textGrid;
        }

        return (fields, grid);
    }

    private static (Dictionary<string, string> Fields, List<Dictionary<string, string>> Grid) ExtractFromWordPositions(
        IReadOnlyList<Word> words)
    {
        var lines = BuildLines(words);
        var (headerIndex, headerColumns) = FindTableHeader(lines);

        if (headerIndex < 0 || headerColumns.Count < 2)
        {
            var onlyText = string.Join(" ", lines.Select(l => l.Text)).Trim();
            return (BuildFieldsFromText(onlyText), []);
        }

        var headers = headerColumns.Select(c => c.Key).ToList();
        var grid = ExtractGridRows(lines, headerIndex, headerColumns, headers);
        var preamble = string.Join(" ", lines.Take(headerIndex).Select(l => l.Text)).Trim();
        return (BuildFieldsFromText(preamble), grid);
    }

    internal static (Dictionary<string, string> Fields, List<Dictionary<string, string>> Grid) ExtractFromPlainText(
        string text)
    {
        var lines = text
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(l => l.Trim())
            .Where(l => l.Length > 0)
            .ToList();

        if (lines.Count == 0)
        {
            return (new Dictionary<string, string>(), []);
        }

        var headerIndex = -1;
        List<string> headers = [];

        for (var i = 0; i < lines.Count; i++)
        {
            var lower = lines[i].ToLowerInvariant();
            var hintCount = HeaderHints.Count(h => lower.Contains(h, StringComparison.Ordinal));
            if (hintCount >= 2)
            {
                headers = SplitTableLine(lines[i]).Select(NormalizeHeader).ToList();
                if (headers.Count >= 3)
                {
                    headerIndex = i;
                    break;
                }
            }
        }

        if (headerIndex < 0)
        {
            return (BuildFieldsFromText(string.Join(" ", lines)), []);
        }

        var grid = new List<Dictionary<string, string>>();
        Dictionary<string, string>? lastRow = null;

        for (var i = headerIndex + 1; i < lines.Count; i++)
        {
            var line = lines[i];
            if (IsFooterOrNoise(line))
            {
                continue;
            }

            if (TryParseDataLine(line, headers, out var row))
            {
                grid.Add(row);
                lastRow = row;
                continue;
            }

            if (lastRow is not null && !StartsWithSerial(line))
            {
                var descriptionKey = FindDescriptionKey(headers);
                if (lastRow.TryGetValue(descriptionKey, out var desc))
                {
                    lastRow[descriptionKey] = $"{desc} {line}".Trim();
                }
            }
        }

        var preamble = string.Join(" ", lines.Take(headerIndex));
        return (BuildFieldsFromText(preamble), grid);
    }

    private static List<LineCluster> BuildLines(IReadOnlyList<Word> words)
    {
        var sorted = words
            .OrderByDescending(w => w.BoundingBox.Bottom)
            .ThenBy(w => w.BoundingBox.Left)
            .ToList();

        var lines = new List<LineCluster>();
        foreach (var word in sorted)
        {
            var y = word.BoundingBox.Bottom;
            var line = lines.FirstOrDefault(l => Math.Abs(l.Y - y) <= RowYTolerance);
            if (line is null)
            {
                line = new LineCluster(y);
                lines.Add(line);
            }

            line.Words.Add(word);
        }

        foreach (var line in lines)
        {
            line.Words.Sort((a, b) => a.BoundingBox.Left.CompareTo(b.BoundingBox.Left));
        }

        lines.Sort((a, b) => b.Y.CompareTo(a.Y));
        return lines;
    }

    private static (int HeaderIndex, List<ColumnRegion> Columns) FindTableHeader(IReadOnlyList<LineCluster> lines)
    {
        for (var i = 0; i < lines.Count; i++)
        {
            var cells = SplitIntoCellsWithBounds(lines[i].Words);
            if (cells.Count < 3)
            {
                continue;
            }

            var headerText = string.Join(" ", cells.Select(c => c.Text)).ToLowerInvariant();
            var hintCount = HeaderHints.Count(h => headerText.Contains(h, StringComparison.Ordinal));
            if (hintCount < 2)
            {
                continue;
            }

            var columns = cells
                .Select(c => new ColumnRegion(NormalizeHeader(c.Text), c.Left, c.Right))
                .ToList();
            return (i, columns);
        }

        return (-1, []);
    }

    private static List<ColumnCell> SplitIntoCellsWithBounds(IReadOnlyList<Word> words)
    {
        if (words.Count == 0)
        {
            return [];
        }

        var threshold = ComputeAdaptiveGapThreshold(words);
        var cells = new List<ColumnCell>
        {
            new(words[0].Text, words[0].BoundingBox.Left, words[0].BoundingBox.Right)
        };

        for (var i = 1; i < words.Count; i++)
        {
            var word = words[i];
            var gap = word.BoundingBox.Left - cells[^1].Right;
            if (gap > threshold)
            {
                cells.Add(new ColumnCell(word.Text, word.BoundingBox.Left, word.BoundingBox.Right));
            }
            else
            {
                var last = cells[^1];
                cells[^1] = last with
                {
                    Text = $"{last.Text} {word.Text}".Trim(),
                    Right = word.BoundingBox.Right
                };
            }
        }

        return cells.Where(c => !string.IsNullOrWhiteSpace(c.Text)).ToList();
    }

    private static double ComputeAdaptiveGapThreshold(IReadOnlyList<Word> words)
    {
        if (words.Count < 2)
        {
            return DefaultCellGapThreshold;
        }

        var gaps = new List<double>();
        for (var i = 1; i < words.Count; i++)
        {
            var gap = words[i].BoundingBox.Left - words[i - 1].BoundingBox.Right;
            if (gap > 0.5)
            {
                gaps.Add(gap);
            }
        }

        if (gaps.Count == 0)
        {
            return DefaultCellGapThreshold;
        }

        gaps.Sort();
        var median = gaps[gaps.Count / 2];
        return Math.Max(4.0, median * 0.75);
    }

    private static List<Dictionary<string, string>> ExtractGridRows(
        IReadOnlyList<LineCluster> lines,
        int headerIndex,
        IReadOnlyList<ColumnRegion> headerColumns,
        IReadOnlyList<string> headers)
    {
        var grid = new List<Dictionary<string, string>>();
        Dictionary<string, string>? lastRow = null;
        var boundaries = BuildColumnBoundaries(headerColumns);

        for (var i = headerIndex + 1; i < lines.Count; i++)
        {
            var cells = AssignWordsToColumns(lines[i].Words, boundaries, headers.Count);
            if (cells.All(string.IsNullOrWhiteSpace))
            {
                continue;
            }

            var joined = string.Join(" ", cells.Where(c => !string.IsNullOrWhiteSpace(c))).Trim();
            if (IsFooterOrNoise(joined))
            {
                continue;
            }

            if (TryBuildRowFromCells(cells, headers, out var row))
            {
                grid.Add(row);
                lastRow = row;
                continue;
            }

            if (lastRow is not null && !StartsWithSerial(joined))
            {
                AppendContinuation(lastRow, headers, cells);
            }
        }

        return grid;
    }

    private static double[] BuildColumnBoundaries(IReadOnlyList<ColumnRegion> columns)
    {
        var boundaries = new double[columns.Count + 1];
        boundaries[0] = columns[0].Left - 1;
        for (var i = 0; i < columns.Count - 1; i++)
        {
            boundaries[i + 1] = (columns[i].Right + columns[i + 1].Left) / 2.0;
        }

        boundaries[^1] = double.MaxValue;
        return boundaries;
    }

    private static string[] AssignWordsToColumns(IReadOnlyList<Word> words, double[] boundaries, int columnCount)
    {
        var cells = Enumerable.Repeat(string.Empty, columnCount).ToArray();
        var builders = Enumerable.Range(0, columnCount).Select(_ => new StringBuilder()).ToArray();

        foreach (var word in words.OrderBy(w => w.BoundingBox.Left))
        {
            var x = word.BoundingBox.Left;
            var columnIndex = columnCount - 1;
            for (var c = 0; c < columnCount; c++)
            {
                if (x >= boundaries[c] && x < boundaries[c + 1])
                {
                    columnIndex = c;
                    break;
                }
            }

            if (builders[columnIndex].Length > 0)
            {
                builders[columnIndex].Append(' ');
            }

            builders[columnIndex].Append(word.Text);
        }

        for (var i = 0; i < columnCount; i++)
        {
            cells[i] = builders[i].ToString().Trim();
        }

        return cells;
    }

    private static bool TryBuildRowFromCells(IReadOnlyList<string> cells, IReadOnlyList<string> headers, out Dictionary<string, string> row)
    {
        row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var nonEmpty = cells.Count(c => !string.IsNullOrWhiteSpace(c));
        if (nonEmpty < 2)
        {
            return false;
        }

        var line = string.Join("  ", cells.Where(c => !string.IsNullOrWhiteSpace(c)));
        if (TryParseDataLine(line, headers, out row))
        {
            return true;
        }

        if (!int.TryParse(cells[0].Trim(), out _))
        {
            return false;
        }

        for (var i = 0; i < headers.Count; i++)
        {
            row[headers[i]] = i < cells.Count ? cells[i].Trim() : string.Empty;
        }

        return true;
    }

    private static void AppendContinuation(
        Dictionary<string, string> lastRow,
        IReadOnlyList<string> headers,
        IReadOnlyList<string> cells)
    {
        var descriptionKey = FindDescriptionKey(headers);
        var extra = string.Join(" ", cells.Where(c => !string.IsNullOrWhiteSpace(c))).Trim();
        if (string.IsNullOrWhiteSpace(extra))
        {
            return;
        }

        if (lastRow.TryGetValue(descriptionKey, out var existing) && !string.IsNullOrWhiteSpace(existing))
        {
            lastRow[descriptionKey] = $"{existing} {extra}".Trim();
        }
        else
        {
            lastRow[descriptionKey] = extra;
        }
    }

    private static bool TryParseDataLine(string line, IReadOnlyList<string> headers, out Dictionary<string, string> row)
    {
        row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var parts = SplitTableLine(line);
        if (parts.Count >= headers.Count && int.TryParse(parts[0], out _))
        {
            for (var i = 0; i < headers.Count; i++)
            {
                row[headers[i]] = parts[i].Trim();
            }

            return true;
        }

        if (!StartsWithSerial(line))
        {
            return false;
        }

        var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length < 5)
        {
            return false;
        }

        if (!int.TryParse(tokens[0], out _))
        {
            return false;
        }

        if (!IsNumericToken(tokens[^1]) || !IsNumericToken(tokens[^2]))
        {
            return false;
        }

        row[headers[0]] = tokens[0];
        row[headers[1]] = tokens[1];
        row[headers[^2]] = tokens[^2];
        row[headers[^1]] = tokens[^1];

        var descriptionKey = FindDescriptionKey(headers);
        row[descriptionKey] = string.Join(' ', tokens.Skip(2).Take(tokens.Length - 4));

        return true;
    }

    private static List<string> SplitTableLine(string line)
    {
        if (line.Contains('\t'))
        {
            return line.Split('\t', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        var byDoubleSpace = Regex.Split(line, @"\s{2,}")
            .Select(p => p.Trim())
            .Where(p => p.Length > 0)
            .ToList();
        if (byDoubleSpace.Count >= 3)
        {
            return byDoubleSpace;
        }

        return line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
    }

    private static bool StartsWithSerial(string line) =>
        Regex.IsMatch(line.TrimStart(), @"^\d+\b");

    private static bool IsNumericToken(string token) =>
        Regex.IsMatch(token.Replace(",", string.Empty), @"^\d+(\.\d+)?$");

    private static bool IsFooterOrNoise(string line)
    {
        var lower = line.ToLowerInvariant();
        return lower.Contains("page ") && lower.Contains(" of ")
               || lower is "total" or "grand total";
    }

    private static string FindDescriptionKey(IReadOnlyList<string> headers) =>
        headers.FirstOrDefault(h => h.Contains("description", StringComparison.OrdinalIgnoreCase))
        ?? (headers.Count > 2 ? headers[2] : "description");

    private static Dictionary<string, string> BuildFieldsFromText(string text)
    {
        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(text))
        {
            return fields;
        }

        fields["message"] = text.Trim();

        var effectiveMatch = Regex.Match(
            text,
            @"w\.?\s*e\.?\s*f\.?\s*(.+?)(?:\s+as\b|\s*$)",
            RegexOptions.IgnoreCase);
        if (effectiveMatch.Success)
        {
            fields["effectiveFrom"] = effectiveMatch.Groups[1].Value.Trim();
        }

        foreach (Match match in Regex.Matches(text, @"([A-Za-z][A-Za-z0-9\s/&\-]{1,40}?)\s*:\s*([^\n\r;]+)"))
        {
            var key = NormalizeHeader(match.Groups[1].Value);
            var value = match.Groups[2].Value.Trim();
            if (!string.IsNullOrWhiteSpace(key) && !fields.ContainsKey(key))
            {
                fields[key] = value;
            }
        }

        return fields;
    }

    private static string NormalizeHeader(string header)
    {
        var cleaned = header.Trim().TrimEnd('.');
        if (string.IsNullOrWhiteSpace(cleaned))
        {
            return "column";
        }

        var slug = Regex.Replace(cleaned.ToLowerInvariant(), @"[^a-z0-9]+", "_").Trim('_');
        return string.IsNullOrWhiteSpace(slug) ? cleaned : slug;
    }

    private sealed record ColumnCell(string Text, double Left, double Right);
    private sealed record ColumnRegion(string Key, double Left, double Right);

    private sealed class LineCluster(double y)
    {
        public double Y { get; } = y;
        public List<Word> Words { get; } = [];
        public string Text => string.Join(" ", Words.Select(w => w.Text));
    }
}
