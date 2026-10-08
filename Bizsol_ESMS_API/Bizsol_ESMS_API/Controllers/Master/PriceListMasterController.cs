using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListMasterController : ControllerBase
    {
        private readonly IPriceListMaster _priceListMaster;

        public PriceListMasterController(IPriceListMaster priceListMaster)
        {
            _priceListMaster = priceListMaster;
        }

        [HttpGet]
        [Route("ShowPriceListMaster")]
        public async Task<IActionResult> ShowPriceListMaster()
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.ShowPriceListMaster(conn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowPriceListMasterByEffectiveDate")]
        public async Task<ActionResult<VM_PriceListMasterForShow>> ShowPriceListMasterByEffectiveDate(string EffectiveDate)
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.ShowPriceListMasterByEffectiveDate(conn, EffectiveDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("ValidatePriceListEffectiveDate")]
        public async Task<IActionResult> ValidatePriceListEffectiveDate(string EffectiveDate)
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.ValidatePriceListEffectiveDate(conn, EffectiveDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowPriceListMasterByCode")]
        public async Task<ActionResult<VM_PriceListMasterForShow>> ShowPriceListMasterByCode(int Code)
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.ShowPriceListMasterByCode(conn, Code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("SavePriceListMaster")]
        public async Task<IActionResult> SavePriceListMaster([FromBody] VM_PriceListMaster model, int UserMaster_Code)
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.SavePriceListMaster(conn, model, UserMaster_Code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("DeletePriceListMaster")]
        public async Task<IActionResult> DeletePriceListMaster(int Code, int UserMaster_Code)
        {
            try
            {
                var conn = CommonFunctions.InitializeERPConnection(HttpContext);
                if (conn.DefultMysqlTemp == null)
                    return StatusCode(500, "Error To Fetch Connection String");

                var result = await _priceListMaster.DeletePriceListMaster(conn, Code, UserMaster_Code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
