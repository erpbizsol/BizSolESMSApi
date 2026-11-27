using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAppIntegrationController : Controller
    {

        private readonly IAccountAppIntegration _AccountAppIntegration;
        public AccountAppIntegrationController(IAccountAppIntegration AccountAppIntegration)
        {
            _AccountAppIntegration = AccountAppIntegration;
        }
        [HttpGet]
        [Route("GetTallybusyTempletConfigrution")]
        public async Task<IActionResult> GetTallybusyTempletConfigrution()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _AccountAppIntegration.GetTallybusyTempletConfigrution(_bizsolESMSConnectionDetails);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "Error To Fetch Connection String");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetTemplateData")]
        public async Task<IActionResult> GetAccountAppIntegrationTemplateData([FromQuery] string Template,[FromQuery] string? FromDate, [FromQuery] string? ToDate)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                var rows = await _AccountAppIntegration.GetAccountAppIntegrationTemplateData(_bizsolESMSConnectionDetails, Template, FromDate, ToDate);
                return Ok(rows);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error To Fetch Connection String");
            }
        }

        [HttpPost("SyncToSql")]
        public async Task<IActionResult> SyncToSql([FromBody] AccountAppSyncRequest req)
        {
            var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
            if (req == null || string.IsNullOrWhiteSpace(req.Template))
                return BadRequest(new[] { new { Status = "N", Msg = "Please select template." } });
            if (req.Codes == null || req.Codes.Count == 0)
                return BadRequest(new[] { new { Status = "N", Msg = "Please select at least one record." } });

            static string CleanDate(string? d) =>
                DateTime.TryParseExact(d, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out var dt) ? dt.ToString("yyyy-MM-dd") : "";

            var result = await _AccountAppIntegration.SyncToSqlServer(
                _bizsolESMSConnectionDetails, req.Template.Trim(), req.TemplateName ?? "",
                CleanDate(req.FromDate), CleanDate(req.ToDate), req.Codes, req.UserCode);

            return Ok(result);
        }
    }


    public class AccountAppSyncRequest
        {
            public string Template { get; set; } = "";
            public string? TemplateName { get; set; }
            public string? FromDate { get; set; }
            public string? ToDate { get; set; }
            public int UserCode { get; set; }
            public List<string> Codes { get; set; } = new();
        }
    
}

