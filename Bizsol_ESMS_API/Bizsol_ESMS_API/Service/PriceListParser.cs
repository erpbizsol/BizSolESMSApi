using System.Text;
using System.Text.RegularExpressions;

namespace Bizsol_ESMS_API.Service;

internal static class PriceListParser
{
    private const string PartNumberPattern = @"(?:\d{6,12}|[A-Z]{2,4}\d{4,10}V?)";

    private static readonly Regex RowRegex = new(
        $@"(?<sn>\d{{1,3}})\s+(?<pn>{PartNumberPattern})\s+(?<desc>.+?)\s+(?<mrp>[\d,]+)\s+(?<dap>[\d,]+\.\d{{2}})" +
        $@"(?=\s+\d{{1,3}}\s+{PartNumberPattern}\s|\s*Thanks\s+and\s+Regards|\s*$)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private static readonly Regex RefNoRegex = new(
        @"Ref\.\s*No\.?\s*:\s*(\S+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex DateRegex = new(
        @"Date\s*:\s*(\d{1,2}(?:th|st|nd|rd)?\s+\w+\s+\d{2,4})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex SubjectRegex = new(
        @"Subject\s*:\s*(.+?)(?=\s+Dear\s+All\b|\s+S/N\.|$)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private static readonly Regex DearAllRegex = new(
        $@"Dear\s+All,\s*(.+?)(?=\s+S/N\.|\s+\d{{1,3}}\s+{PartNumberPattern}\s)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private static readonly Regex EffectiveFromRegex = new(
        @"w\.?\s*e\.?\s*f\.?\s*(\d{1,2}(?:th|st|nd|rd)?\s+\w+\s+\d{2,4})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static (Dictionary<string, string> Fields, List<Dictionary<string, string>> Grid) TryParse(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return (new Dictionary<string, string>(), []);
        }

        var normalized = Normalize(text);
        var grid = ExtractRows(normalized);
        if (grid.Count == 0)
        {
            return (new Dictionary<string, string>(), []);
        }

        var fields = ExtractHeaderFields(normalized);
        return (fields, grid);
    }

    public static List<Dictionary<string, string>> ExtractRows(string text)
    {
        var normalized = Normalize(text);
        var grid = new List<Dictionary<string, string>>();

        foreach (Match match in RowRegex.Matches(normalized))
        {
            grid.Add(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["s_n"] = match.Groups["sn"].Value.Trim(),
                ["ffpl_p_n"] = match.Groups["pn"].Value.Trim(),
                ["description"] = CollapseSpaces(match.Groups["desc"].Value),
                ["revised_mrp"] = match.Groups["mrp"].Value.Trim(),
                ["revised_dap"] = match.Groups["dap"].Value.Trim()
            });
        }

        return grid;
    }

    private static Dictionary<string, string> ExtractHeaderFields(string text)
    {
        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (text.Contains("MARKET COMMUNICATION", StringComparison.OrdinalIgnoreCase))
        {
            fields["documentTitle"] = "MARKET COMMUNICATION";
        }

        var refMatch = RefNoRegex.Match(text);
        if (refMatch.Success)
        {
            fields["refNo"] = refMatch.Groups[1].Value.Trim();
        }

        var dateMatch = DateRegex.Match(text);
        if (dateMatch.Success)
        {
            fields["date"] = CollapseSpaces(dateMatch.Groups[1].Value);
        }

        var subjectMatch = SubjectRegex.Match(text);
        if (subjectMatch.Success)
        {
            fields["subject"] = CollapseSpaces(subjectMatch.Groups[1].Value);
        }

        var dearMatch = DearAllRegex.Match(text);
        if (dearMatch.Success)
        {
            fields["message"] = CollapseSpaces(dearMatch.Groups[1].Value);
        }

        var effectiveMatch = EffectiveFromRegex.Match(text);
        if (effectiveMatch.Success)
        {
            fields["effectiveFrom"] = CollapseSpaces(effectiveMatch.Groups[1].Value);
        }

        return fields;
    }

    private static string Normalize(string text)
    {
        var builder = new StringBuilder(text.Length);
        foreach (var ch in text)
        {
            if (ch is '\r' or '\n' or '\t')
            {
                builder.Append(' ');
            }
            else
            {
                builder.Append(ch);
            }
        }

        return CollapseSpaces(builder.ToString());
    }

    private static string CollapseSpaces(string value) =>
        Regex.Replace(value.Trim(), @"\s{2,}", " ");
}
