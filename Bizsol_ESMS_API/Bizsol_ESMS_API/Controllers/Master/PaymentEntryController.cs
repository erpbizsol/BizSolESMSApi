using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentEntryController : ControllerBase
    {
        private readonly IPaymentEntry _PaymentEntry;

        public PaymentEntryController(IPaymentEntry paymentEntry)
        {
            _PaymentEntry = paymentEntry;
        }

        #region PaymentEntry

        [HttpPost]
        [Route("SavePaymentEntry")]
        public async Task<IActionResult> SavePaymentEntry([FromBody] VM_BillMaster vmBillMaster, int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.SavePaymentEntry(_bizsolESMSConnectionDetails, vmBillMaster, UserMaster_Code);
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
        [Route("GetPaymentMasterlist")]
        public async Task<IActionResult> GetPaymentMasterlist(string FromDate, string ToDate, int AccountMaster_Code = 0, string PaymentMode = "")
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPaymentMasterlist(_bizsolESMSConnectionDetails, FromDate, ToDate, AccountMaster_Code, PaymentMode);
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
        [Route("ShowPaymentEntryByCode")]
        public async Task<ActionResult<VM_BillMasterList>> ShowPaymentEntryByCode(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPaymentEntryByCode(_bizsolESMSConnectionDetails, Code);
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
        [Route("DeletePaymentEntry")]
        public async Task<IActionResult> DeletePaymentEntry(int Code, int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.DeletePaymentEntry(_bizsolESMSConnectionDetails, Code, UserMaster_Code);
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
        [Route("GetInvoiceDetailsByAccountMaster")]
        public async Task<IActionResult> GetInvoiceDetailsByAccountMaster(int AccountMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetInvoiceDetailsByAccountMaster(_bizsolESMSConnectionDetails, AccountMaster_Code);
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
        [Route("GetPendingInvoiceReport")]
        public async Task<IActionResult> GetPendingInvoiceReport(string AsonDate, int AccountMaster_Code = 0)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPendingInvoiceReport(_bizsolESMSConnectionDetails, AccountMaster_Code, AsonDate);
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

        #endregion PaymentEntry

        #region PaymentEntryAdjustment
        [HttpPost]
        [Route("SavePaymentEntryAdjustment")]
        public async Task<IActionResult> SavePaymentEntryAdjustment([FromBody] VM_BillMaster vmBillMaster, int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.SavePaymentEntryAdjustment(_bizsolESMSConnectionDetails, vmBillMaster, UserMaster_Code);
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
        [Route("GetPaymentAdjustmentMasterlist")]
        public async Task<IActionResult> GetPaymentAdjustmentMasterlist(string FromDate, string ToDate, int AccountMaster_Code = 0, string PaymentMode = "")
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPaymentAdjustmentMasterlist(_bizsolESMSConnectionDetails, FromDate, ToDate, AccountMaster_Code, PaymentMode);
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
        [Route("ShowPaymentEntryAdjustmentByCode")]
        public async Task<ActionResult<VM_BillMasterList>> ShowPaymentEntryAdjustmentByCode(int Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPaymentEntryAdjustmentByCode(_bizsolESMSConnectionDetails, Code);
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
        [Route("DeletePaymentEntryAdjustment")]
        public async Task<IActionResult> DeletePaymentEntryAdjustment(int Code, int UserMaster_Code)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.DeletePaymentEntryAdjustment(_bizsolESMSConnectionDetails, Code, UserMaster_Code);
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
        [Route("GetPaymentModeList")]
        public async Task<IActionResult> GetPaymentModeList()
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _PaymentEntry.GetPaymentModeList(_bizsolESMSConnectionDetails);
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
        #endregion PaymentEntryAdjustment
    }
}
