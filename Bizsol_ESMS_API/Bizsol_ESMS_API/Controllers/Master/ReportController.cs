using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Bizsol_ESMS_API.Interface;
using Microsoft.Graph.Models;
using System.Composition;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReport _report;
        public ReportController(IReport report)
        {
            _report = report;

        }
        [HttpGet]
        [Route("GetLocationReport")]
        public async Task<IActionResult> GetLocationReport(string Templete)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _report.GetLocationReport(_bizsolESMSConnectionDetails, Templete);
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
        [Route("GetReportType")]
        public async Task<IActionResult> GetReportType(string ModuleDesp)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _report.GetReportType(_bizsolESMSConnectionDetails, ModuleDesp);
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
        [Route("GetStockLedgerList")]
        public async Task<IActionResult> GetStockLedgerList([FromBody] tblStockLedger StockLedger)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _report.GetStockLedgerList(_bizsolESMSConnectionDetails, StockLedger);
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
        [Route("GetStockAuditMaster")]
        public async Task<IActionResult> GetStockAuditMaster(string FromDate,string ToDate)
        {
            try
            {
                var _bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (_bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _report.GetStockAuditMaster(_bizsolESMSConnectionDetails,FromDate,ToDate);
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
