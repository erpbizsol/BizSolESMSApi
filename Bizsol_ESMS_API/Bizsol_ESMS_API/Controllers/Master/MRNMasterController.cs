using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class MRNMasterController : ControllerBase
    {
        private readonly IMRNMaster _MRNMaster;

        public MRNMasterController(IMRNMaster MRNMaster)
        {
            _MRNMaster = MRNMaster;
        }
        #region MRNMaster

        [HttpPost]
        [Route("SaveMRNMaster")]
        public async Task<IActionResult> SaveMRNMaster([FromBody] VM_MRNMaster vmMRNMaster,int UserMaster_Code)
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.SaveMRNMaster(_bizsolESMSConnectionDetails, vmMRNMaster, UserMaster_Code);
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
        [Route("GetMRNMasterList")]
        public async Task<IActionResult> GetMRNMasterList()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetMRNMasterList(_bizsolESMSConnectionDetails);
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
        [Route("ShowMRNMasterByCode")]
        public async Task<ActionResult<VM_MRNMasterList>> ShowMRNMasterByCode(int Code)
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetMRNMasterByCode(_bizsolESMSConnectionDetails, Code);
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
        [Route("DeleteMRNMaster")]
        public async Task<ActionResult<spOutputParameter>> DeleteMRNMaster(int Code,int UserMaster_Code)
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.DeleteMRNMaster(_bizsolESMSConnectionDetails, Code, UserMaster_Code);
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
        [Route("GetRateByVendor")]
        public async Task<IActionResult> GetRateByVendor(string VendorName,string ItemName)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetRateByVendor(_bizsolESMSConnectionDetails,VendorName, ItemName);
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
        [Route("ImportMRNMaster")]
        public async Task<IActionResult> ImportMRNMaster([FromBody] tblImportMRNMaster ImportMRNMaster)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.ImportMRNMaster(_bizsolESMSConnectionDetails, ImportMRNMaster);
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
        [Route("ImportMRNMasterForTemp")]
        public async Task<IActionResult> ImportMRNMasterForTemp([FromBody] tblImportMRNMaster ImportMRNMaster)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.ImportMRNMasterForTemp(_bizsolESMSConnectionDetails, ImportMRNMaster);
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

        #endregion MRNMaster

    }
}
