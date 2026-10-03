using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerReplacementOutController : ControllerBase
    {
        private readonly IRetailerReplacementOut _retailerReplacementOut;

        public RetailerReplacementOutController(IRetailerReplacementOut retailerReplacementOut)
        {
            _retailerReplacementOut = retailerReplacementOut;
        }

        [HttpGet]
        [Route("ShowRetailerReplacementOut")]
        public async Task<IActionResult> ShowRetailerReplacementOut()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementOut.ShowRetailerReplacementOut(connection);
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
        [Route("ShowRetailerReplacementOutByCode")]
        public async Task<ActionResult<VM_RetailerReplacementOutList>> ShowRetailerReplacementOutByCode(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementOut.ShowRetailerReplacementOutByCode(connection, Code);
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
        public async Task<ActionResult<VM_RetailerReplacementOutList>> ShowDataByEntryNo(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementOut.ShowDataByEntryNo(connection, Code);
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
                    var result = await _retailerReplacementOut.GetEntryNoList(connection);
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
                    var result = await _retailerReplacementOut.GetBrandMasterListForReplacement(connection);
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
        [Route("SaveRetailerReplacementOut")]
        public async Task<IActionResult> SaveRetailerReplacementOut([FromBody] VM_RetailerReplacementOut model)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementOut.SaveRetailerReplacementOut(connection, model);
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
        [Route("DeleteRetailerReplacementOut")]
        public async Task<IActionResult> DeleteRetailerReplacementOut(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementOut.DeleteRetailerReplacementOut(connection, Code);
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
