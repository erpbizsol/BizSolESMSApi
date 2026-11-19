using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAuditmasterController : Controller
    {
        private readonly IStockAuditmaster _StockAuditmaster;

        #region StockAuditmaster
       
        public StockAuditmasterController(IStockAuditmaster StockAuditmaster)
        {
            _StockAuditmaster = StockAuditmaster;
        }
        [HttpGet]
        [Route("GetStockAuditmasterList")]
        public async Task<IActionResult> GetStockAuditmasterList()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _StockAuditmaster.GetStockAuditmasterList(_bizsolESMSConnectionDetails);
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
        [HttpPost]
        [Route("ScanStockAuditmaster")]
        public async Task<IActionResult> ScanStockAuditmaster([FromBody] tblStockAuditmaster StockAuditmaster)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _StockAuditmaster.ScanStockAuditmaster(_bizsolESMSConnectionDetails, StockAuditmaster);
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
        [HttpPost]
        [Route("CompleteStockAuditmaster")]
        public async Task<IActionResult> CompleteStockAuditmaster([FromBody] int Code, int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _StockAuditmaster.CompleteStockAuditmaster(_bizsolESMSConnectionDetails,Code, UserMaster_Code);
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
        #endregion StockAuditmaster
    }
}
