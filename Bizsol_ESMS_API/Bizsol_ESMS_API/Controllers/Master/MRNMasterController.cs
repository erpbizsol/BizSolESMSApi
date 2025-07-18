﻿using Bizsol_ESMS_API.Interface;
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
        public async Task<IActionResult> GetMRNMasterList(string FromDate,string ToDate)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetMRNMasterList(_bizsolESMSConnectionDetails, FromDate,ToDate);
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

        [HttpGet]
        [Route("ExportMRNMaster")]
        public async Task<IActionResult> ExportMRNMaster(string FromDate, string ToDate)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.ExportMRNMaster(_bizsolESMSConnectionDetails, FromDate, ToDate);
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
        [Route("MRNDetailsByCode")]
        public async Task<ActionResult> MRNDetailsByCode(int Code)
        {

            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.MRNDetailsByCode(_bizsolESMSConnectionDetails, Code);
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
        [Route("MRNDetailsByVehicleNo")]
        public async Task<ActionResult> MRNDetailsByVehicleNo(string MRNDate,string VehicleNo)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.MRNDetailsByVehicleNo(_bizsolESMSConnectionDetails, MRNDate, VehicleNo);
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

        #region BoxUnloading

        [HttpPost]
        [Route("BoxUnloading")]
        public async Task<IActionResult> BoxUnloading([FromBody] tblBoxUnloading BoxUnloading)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.BoxUnloading(_bizsolESMSConnectionDetails, BoxUnloading);
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
        [Route("GetMRNDetailForUnloading")]
        public async Task<IActionResult> GetMRNDetailForUnloading()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetMRNDetailForUnloading(_bizsolESMSConnectionDetails);
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
        [Route("ManualBoxUnloading")]
        public async Task<IActionResult> ManualBoxUnloading(int BoxNo,string PicklistNo,string IsManual)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.ManualBoxUnloading(_bizsolESMSConnectionDetails, BoxNo,PicklistNo,IsManual);
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

        #endregion BoxUnloading

        #region BoxValidation

        [HttpPost]
        [Route("GetBoxValidateDetail")]
        public async Task<IActionResult> GetBoxValidateDetail([FromBody] tblBoxValidation BoxValidation)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetBoxValidateDetail(_bizsolESMSConnectionDetails, BoxValidation);
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
        [Route("SaveManualBoxValidateDetail")]
        public async Task<IActionResult> SaveManualBoxValidateDetail([FromBody] tblBoxValidation BoxValidation)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.SaveManualBoxValidateDetail(_bizsolESMSConnectionDetails, BoxValidation);
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
        [Route("SaveScanBoxValidateDetail")]
        public async Task<IActionResult> SaveScanBoxValidateDetail([FromBody] tblBoxValidation BoxValidation)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.SaveScanBoxValidateDetail(_bizsolESMSConnectionDetails, BoxValidation);
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
        [Route("AutoUpdateReceivedQty")]
        public async Task<IActionResult> AutoUpdateReceivedQty([FromBody] tblBoxValidation BoxValidation)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.AutoUpdateReceivedQty(_bizsolESMSConnectionDetails, BoxValidation);
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
        [Route("GetMRNDetailForValidate")]
        public async Task<IActionResult> GetMRNDetailForValidate()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetMRNDetailForValidate(_bizsolESMSConnectionDetails);
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
        [Route("GetExportBoxUnloading")]
        public async Task<IActionResult> GetExportBoxUnloading(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetExportBoxUnloading(_bizsolESMSConnectionDetails,Code);
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
        [Route("GetVehicleNoList")]
        public async Task<IActionResult> GetVehicleNoList()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _MRNMaster.GetVehicleNoList(_bizsolESMSConnectionDetails);
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
        #endregion BoxValidation
    }
}
