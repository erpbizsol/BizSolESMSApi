using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerReplacementInController : ControllerBase
    {
        private readonly IRetailerReplacementIn _retailerReplacementIn;

        public RetailerReplacementInController(IRetailerReplacementIn retailerReplacementIn)
        {
            _retailerReplacementIn = retailerReplacementIn;
        }

        [HttpGet]
        [Route("ShowRetailerReplacementIn")]
        public async Task<IActionResult> ShowRetailerReplacementIn()
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementIn.ShowRetailerReplacementIn(connection);
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
        [Route("ShowRetailerReplacementInByCode")]
        public async Task<ActionResult<VM_RetailerReplacementInList>> ShowRetailerReplacementInByCode(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementIn.ShowRetailerReplacementInByCode(connection, Code);
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
        [Route("SaveRetailerReplacementIn")]
        public async Task<IActionResult> SaveRetailerReplacementIn([FromBody] VM_RetailerReplacementIn model)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementIn.SaveRetailerReplacementIn(connection, model);
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
        [Route("DeleteRetailerReplacementIn")]
        public async Task<IActionResult> DeleteRetailerReplacementIn(int Code)
        {
            try
            {
                var connection = CommonFunctions.InitializeERPConnection(HttpContext);
                if (connection.DefultMysqlTemp != null)
                {
                    var result = await _retailerReplacementIn.DeleteRetailerReplacementIn(connection, Code);
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
                    var result = await _retailerReplacementIn.GetBrandMasterListForReplacement(connection);
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
