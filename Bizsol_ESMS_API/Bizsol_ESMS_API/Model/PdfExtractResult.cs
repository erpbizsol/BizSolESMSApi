namespace Bizsol_ESMS_API.Model;

public sealed class PdfExtractResult
{
    public bool Success { get; init; } = true;
    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();
    public string FileName { get; init; } = string.Empty;
    public long FileSizeBytes { get; init; }
    public int PageCount { get; init; }
    public PdfMetadata Metadata { get; init; } = new();
    /// <summary>Non-table text as key-value pairs (e.g. message, effectiveFrom).</summary>
    public IReadOnlyDictionary<string, string> Fields { get; init; } = new Dictionary<string, string>();
    /// <summary>Table/grid rows; each item is one row with column keys.</summary>
    public IReadOnlyList<IReadOnlyDictionary<string, string>> Grid { get; init; } = Array.Empty<IReadOnlyDictionary<string, string>>();
}

public sealed class PdfMetadata
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public string? Subject { get; init; }
    public string? Keywords { get; init; }
    public string? Creator { get; init; }
    public string? Producer { get; init; }
    public string? CreationDate { get; init; }
    public string? ModificationDate { get; init; }
}
