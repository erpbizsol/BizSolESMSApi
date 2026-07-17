using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanToBillController : ControllerBase
    {
        private readonly IScanToBill _ScanToBill;

        public ScanToBillController(IScanToBill scanToBill)
        {
            _ScanToBill = scanToBill;
        }

        [HttpPost]
        [Route("SaveItemScanToBill")]
        public async Task<IActionResult> SaveItemScanToBill([FromBody] tblScanToBill Dispatch)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.SaveItemScanToBill(_bizsolESMSConnectionDetails, Dispatch);
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
        [Route("ManuaItemScanToBill")]
        public async Task<IActionResult> ManuaItemScanToBill([FromBody] tblScanDispatch Dispatch)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.ManuaItemScanToBill(_bizsolESMSConnectionDetails, Dispatch);
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

        [HttpGet]
        [Route("GetDetailsItemScanToBill")]
        public async Task<ActionResult<VM_OrderMasterForShow>> GetDetailsItemScanToBill(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.GetDetailsItemScanToBill(_bizsolESMSConnectionDetails, Code);
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
        [Route("AddItemScanToBill")]
        public async Task<ActionResult> AddItemScanToBill([FromBody] tblAddItemScanToBill AddItemScanToBill)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.AddItemScanToBill(_bizsolESMSConnectionDetails, AddItemScanToBill);
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
        [Route("SaveManualRateAndQtySacnToBill")]
        public async Task<IActionResult> SaveManualRateAndQtySacnToBill([FromBody] tblManualRateAndQty Dispatch)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.SaveManualRateAndQtySacnToBill(_bizsolESMSConnectionDetails, Dispatch);
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
        [HttpGet]
        [Route("DeleteItemFormScanToBill")]
        public async Task<ActionResult> DeleteItemFormScanToBill(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _ScanToBill.DeleteItemFormScanToBill(_bizsolESMSConnectionDetails, Code);
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
    }
}
