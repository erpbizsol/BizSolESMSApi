using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface;

public interface IPdfExtractionService
{
    PdfExtractResult Extract(Stream pdfStream, string fileName, long fileSizeBytes);
}
