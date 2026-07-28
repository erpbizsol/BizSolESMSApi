using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketMasterController : ControllerBase
    {
        private readonly ITicketMaster _ticketMaster;

        public TicketMasterController(ITicketMaster ticketMaster)
        {
            _ticketMaster = ticketMaster;
        }

        [HttpPost]
        [Route("Create")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> CreateTicket([FromForm] CreateTicketRequest request,[FromForm] List<IFormFile>? Files)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                var result = await _ticketMaster.CreateTicket(bizsolESMSConnectionDetails, request, Files);

                if (!result.Success)
                {
                    if (result.Message != null &&
                        (result.Message.Contains("required", StringComparison.OrdinalIgnoreCase) ||
                         result.Message.Contains("valid log date", StringComparison.OrdinalIgnoreCase)))
                    {
                        return BadRequest(result);
                    }

                    if (result.Message != null &&
                        result.Message.Contains("could not be created", StringComparison.OrdinalIgnoreCase))
                    {
                        return Conflict(result);
                    }

                    return StatusCode(500, result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CreateTicketResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("AttechedFileChecks")]
        public async Task<IActionResult> AttechedFileChecks(string CompanyCode, long TicketNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CompanyCode))
                {
                    return BadRequest("Company code is required.");
                }

                if (TicketNo <= 0)
                {
                    return BadRequest("Valid ticket no is required.");
                }

                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                var result = await _ticketMaster.AttechedFileChecks(bizsolESMSConnectionDetails, CompanyCode, TicketNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
