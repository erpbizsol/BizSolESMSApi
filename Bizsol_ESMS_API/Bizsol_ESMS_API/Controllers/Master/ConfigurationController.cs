using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : Controller
    {
        private readonly IPrefixConfiguration _prefixConfiguration;
        public ConfigurationController(IPrefixConfiguration prefixConfiguration)
        {
            _prefixConfiguration = prefixConfiguration;
        }
        [HttpGet]
        [Route("GetPrefixConfigurationList")]
        public async Task<IActionResult> GetPrefixConfigurationList()
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _prefixConfiguration.GetPrefixConfigurationList(_bizsolESMSConnectionDetails);
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
        [Route("SavePrefixConfiguration")]
        public async Task<IActionResult> SavePrefixConfiguration([FromBody] tblPrefixConfiguration model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _prefixConfiguration.SavePrefixConfiguration(_bizsolESMSConnectionDetails, model);
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
        [Route("GetPerPageSizeList")]
        public async Task<IActionResult> GetPerPageSizeList()
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _prefixConfiguration.GetPerPageSizeList(_bizsolESMSConnectionDetails);
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
        [Route("SavePerPageSizeConfiguration")]
        public async Task<IActionResult> SavePerPageSizeConfiguration([FromBody] tblPerPageSize model)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _prefixConfiguration.SavePerPageSizeConfiguration(_bizsolESMSConnectionDetails, model);
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
