﻿using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : Controller
    {
        private readonly IUserMaster _UserMaster;

        #region UserMaster
        public UserMasterController(IUserMaster userMaster)
        {
            _UserMaster = userMaster;
        }
        [HttpGet]
        [Route("GetUserMasterList")]
        public async Task<IActionResult> GetUserMasterList(int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.GetUserMasterList(_bizsolESMSConnectionDetails.DefultMysqlTemp,UserMaster_Code);
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
        [Route("SaveUserMaster")]
        public async Task<IActionResult> SaveUserMaster([FromBody] tblUserMaster _tblUserMaster)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.SaveUserMaster(_bizsolESMSConnectionDetails.DefultMysqlTemp, _tblUserMaster);
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
        [Route("GetUserMasterListByCode")]
        public async Task<IActionResult> GetUserMasterListByCode(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.GetUserMasterListByCode(_bizsolESMSConnectionDetails.DefultMysqlTemp,Code);
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
        [Route("DeleteUserMaster")]
        public async Task<IActionResult> DeleteUserMaster(int Code,int UserMaster_Code,string Reason)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.DeleteUserMaster(_bizsolESMSConnectionDetails.DefultMysqlTemp,Code,UserMaster_Code,Reason);
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
        [Route("GetUserModuleMasterList")]
        public async Task<IActionResult> GetUserModuleMasterList()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.GetUserModuleMasterList(_bizsolESMSConnectionDetails.DefultMysqlTemp);
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
        [Route("SaveUserModuleMaster")]
        public async Task<IActionResult> SaveUserModuleMaster([FromBody] IEnumerable<tblUserModuleMaster> _tblUserModuleMaster)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.SaveUserModuleMaster(_bizsolESMSConnectionDetails.DefultMysqlTemp, _tblUserModuleMaster);
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
        [Route("GetUserOptionsDetails")]
        public async Task<IActionResult> GetUserOptionsDetails()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.GetUserOptionsDetails(_bizsolESMSConnectionDetails.DefultMysqlTemp);
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
        [Route("GetUserModuleRightsList")]
        public async Task<IActionResult> GetUserModuleRightsList(int CompanyCode, int UserCode)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.GetUserModuleRightsList(_bizsolESMSConnectionDetails.DefultMysqlTemp,CompanyCode,UserCode);
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
        [Route("CheckUserOptionRight")]
        public async Task<IActionResult> CheckUserOptionRight(int UserModuleMaster_Code, int UserMaster_Code,string Option)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _UserMaster.CheckUserOptionRight(_bizsolESMSConnectionDetails.DefultMysqlTemp, UserModuleMaster_Code, UserMaster_Code,Option);
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

        #endregion UserMaster
    }
}
