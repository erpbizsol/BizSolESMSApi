using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorReplacementOutController : ControllerBase
    {
        private readonly IVendorReplacementOut _vendorReplacementOut;

        public VendorReplacementOutController(IVendorReplacementOut vendorReplacementOut)
        {
            _vendorReplacementOut = vendorReplacementOut;
        }

        [HttpGet]
        [Route("ShowVendorReplacementOut")]
        public async Task<IActionResult> ShowVendorReplacementOut()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementOut.ShowVendorReplacementOut(connection);
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
        [Route("ShowVendorReplacementOutByCode")]
        public async Task<ActionResult<VM_VendorReplacementOutList>> ShowVendorReplacementOutByCode(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementOut.ShowVendorReplacementOutByCode(connection, Code);
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
        [Route("SaveVendorReplacementOut")]
        public async Task<IActionResult> SaveVendorReplacementOut([FromBody] VM_VendorReplacementOut model)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementOut.SaveVendorReplacementOut(connection, model);
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
        [Route("DeleteVendorReplacementOut")]
        public async Task<IActionResult> DeleteVendorReplacementOut(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementOut.DeleteVendorReplacementOut(connection, Code);
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
        [Route("GetBrandMasterListForReplacement")]
        public async Task<ActionResult<dynamic>> GetBrandMasterListForReplacement()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _vendorReplacementOut.GetBrandMasterListForReplacement(connection);
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
