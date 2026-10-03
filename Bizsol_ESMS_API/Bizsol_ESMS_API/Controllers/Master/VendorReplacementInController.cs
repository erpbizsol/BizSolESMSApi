using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorReplacementInController : ControllerBase
    {
        private readonly IVendorReplacementIn _vendorReplacementIn;

        public VendorReplacementInController(IVendorReplacementIn vendorReplacementIn)
        {
            _vendorReplacementIn = vendorReplacementIn;
        }

        [HttpGet]
        [Route("ShowVendorReplacementIn")]
        public async Task<IActionResult> ShowVendorReplacementIn()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.ShowVendorReplacementIn(connection);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowVendorReplacementInByCode")]
        public async Task<ActionResult<VM_VendorReplacementInList>> ShowVendorReplacementInByCode(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.ShowVendorReplacementInByCode(connection, Code);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowDataByEntryNo")]
        public async Task<ActionResult<VM_VendorReplacementInList>> ShowDataByEntryNo(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.ShowDataByEntryNo(connection, Code);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEntryNoList")]
        public async Task<ActionResult<dynamic>> GetEntryNoList()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.GetEntryNoList(connection);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveVendorReplacementIn")]
        public async Task<IActionResult> SaveVendorReplacementIn([FromBody] VM_VendorReplacementIn model)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.SaveVendorReplacementIn(connection, model);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteVendorReplacementIn")]
        public async Task<IActionResult> DeleteVendorReplacementIn(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementIn.DeleteVendorReplacementIn(connection, Code);
                    return Ok(result);
                }
                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
