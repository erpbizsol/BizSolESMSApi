// JS (WarehouseTransfer.js) in endpoints ko call karti hai:
//      GET  api/WarehouseStockTransfer/GetList
//      GET  api/WarehouseStockTransfer/GetDetails
//      POST api/WarehouseStockTransfer/ScanItem
//      POST api/WarehouseStockTransfer/UpdateHeader
using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseStockTransferController : ControllerBase
    {
        private readonly IWarehouseStockTransferService _IWarehouseStockTransfer;

        public WarehouseStockTransferController(IWarehouseStockTransferService IWarehouseStockTransfer)
        {
            _IWarehouseStockTransfer = IWarehouseStockTransfer;
        }

        #region IWarehouseStockTransfer

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList(string? FromDate, string? ToDate, int WarehouseMaster_Code = 0, int UserMaster_Code = 0)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.GetList(_bizsolESMSConnectionDetails, FromDate, ToDate, WarehouseMaster_Code, UserMaster_Code);
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
        [Route("GetDetails")]
        public async Task<IActionResult> GetDetails(int Code, int UserMaster_Code = 0)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.GetDetails(_bizsolESMSConnectionDetails, Code, UserMaster_Code);
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
        [Route("ScanItem")]
        public async Task<IActionResult> ScanItem([FromBody] WarehouseTransferScanModel model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.ScanItem(_bizsolESMSConnectionDetails, model);
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
        [Route("UpdateHeader")]
        public async Task<IActionResult> UpdateHeader([FromBody] WarehouseTransferHeaderModel model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.UpdateHeader(_bizsolESMSConnectionDetails, model);
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
        [Route("Complete")]
        public async Task<IActionResult> Complete([FromBody] WarehouseTransferCompleteModel model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.CompleteTransfer(_bizsolESMSConnectionDetails, model);
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
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] WarehouseTransferCompleteModel model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.DeleteTransfer(_bizsolESMSConnectionDetails, model);
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
        [Route("GetPrintData")]
        public async Task<IActionResult> GetPrintData(int Code, int UserMaster_Code = 0)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _IWarehouseStockTransfer.GetPrintData(_bizsolESMSConnectionDetails, Code, UserMaster_Code);
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
        #endregion IWarehouseStockTransfer
    }
}
