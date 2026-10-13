using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using UglyToad.PdfPig;

namespace Bizsol_ESMS_API.Service;

public sealed class PdfExtractionService : IPdfExtractionService
{
    public PdfExtractResult Extract(Stream pdfStream, string fileName, long fileSizeBytes)
    {
        using var document = PdfDocument.Open(pdfStream);

        var fullTextBuilder = new System.Text.StringBuilder();

        foreach (var page in document.GetPages())
        {
            var text = page.Text ?? string.Empty;

            if (fullTextBuilder.Length > 0)
            {
                fullTextBuilder.AppendLine();
            }

            fullTextBuilder.Append(text);
        }

        var fullText = fullTextBuilder.ToString();
        var (priceFields, priceGrid) = PriceListParser.TryParse(fullText);
        var usePriceList = priceGrid.Count > 0;

        var allFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var allGrid = new List<IReadOnlyDictionary<string, string>>();

        if (usePriceList)
        {
            foreach (var pair in priceFields)
            {
                allFields[pair.Key] = pair.Value;
            }

            allGrid.AddRange(priceGrid);
        }
        else
        {
            foreach (var page in document.GetPages())
            {
                var (fields, grid) = PdfStructuredExtractor.ExtractPage(page);

                foreach (var pair in fields)
                {
                    allFields[pair.Key] = pair.Value;
                }

                allGrid.AddRange(grid);
            }
        }

        var info = document.Information;
        var metadata = new PdfMetadata
        {
            Title = NullIfEmpty(info?.Title),
            Author = NullIfEmpty(info?.Author),
            Subject = NullIfEmpty(info?.Subject),
            Keywords = NullIfEmpty(info?.Keywords),
            Creator = NullIfEmpty(info?.Creator),
            Producer = NullIfEmpty(info?.Producer),
            CreationDate = info?.CreationDate is null ? null : info.CreationDate.ToString(),
            ModificationDate = info?.ModifiedDate is null ? null : info.ModifiedDate.ToString()
        };

        return new PdfExtractResult
        {
            Success = true,
            Errors = Array.Empty<string>(),
            FileName = fileName,
            FileSizeBytes = fileSizeBytes,
            PageCount = document.NumberOfPages,
            Metadata = metadata,
            Fields = allFields,
            Grid = allGrid
        };
    }

    private static string? NullIfEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
