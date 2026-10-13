using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfExtractionService _pdfExtractionService;
        private readonly ILogger<PdfController> _logger;

        public PdfController(IPdfExtractionService pdfExtractionService, ILogger<PdfController> logger)
        {
            _pdfExtractionService = pdfExtractionService;
            _logger = logger;
        }

        /// <summary>
        /// Upload a PDF file and receive extracted fields and grid rows as JSON.
        /// </summary>
        [HttpPost("extract")]
        [RequestSizeLimit(52_428_800)] // 50 MB
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> Extract(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return Ok(EmptyResult(file?.FileName ?? string.Empty, file?.Length ?? 0));
            }

            try
            {
                await using var stream = file.OpenReadStream();
                var result = _pdfExtractionService.Extract(stream, file.FileName, file.Length);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read PDF {FileName}", file.FileName);
                return Ok(EmptyResult(file.FileName, file.Length));
            }
        }

        private static PdfExtractResult EmptyResult(string fileName, long fileSizeBytes) =>
            new()
            {
                Success = true,
                Errors = Array.Empty<string>(),
                FileName = fileName,
                FileSizeBytes = fileSizeBytes,
                PageCount = 0,
                Metadata = new PdfMetadata(),
                Fields = new Dictionary<string, string>(),
                Grid = Array.Empty<IReadOnlyDictionary<string, string>>()
            };
    }
}
