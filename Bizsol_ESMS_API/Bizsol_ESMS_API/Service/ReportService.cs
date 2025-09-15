using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
namespace Bizsol_ESMS_API.Service
{
    public class ReportService : IReport
    {
        public async Task<IEnumerable<dynamic>> GetLocationReport(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string Templete)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var data = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                    _bizsolESMSConnectionDetails.DefultMysqlTemp,
                    $"SELECT ProcedureName FROM Location_information WHERE ReportName=@ReportName",
                    new Dictionary<string, object> { { "@ReportName", Templete } })[0];

                if (data.Rows.Count > 0)
                {
                    string procedureName = data.Rows[0]["ProcedureName"].ToString();
                    var result = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                        _bizsolESMSConnectionDetails.DefultMysqlTemp,
                        $"CALL {procedureName}(@p_ReportName)",
                        new Dictionary<string, object> { { "@p_ReportName", Templete } })[0];
                    return CommonFunctions.DatatableToDynamicList(result);
                }
                return new List<dynamic>();
            }
        }
        public async Task<IEnumerable<dynamic>> GetReportType(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string ModuleDesp)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                string query = $"SELECT Code, DisplayName FROM F_ReportConfiguration WHERE ModuleDescription = '{ModuleDesp.Trim()}'";
                DynamicParameters parameters = new DynamicParameters();
                    var result = await conn.QueryAsync<dynamic>(query, parameters, commandType: CommandType.Text);
                    return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetStockLedgerList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,tblStockLedger StockLedger)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var data = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                    _bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "SELECT ProcedureName FROM F_ReportConfiguration WHERE ModuleDescription = 'Stock Ledger' AND DisplayName = @DisplayName",
                    new Dictionary<string, object> { { "@DisplayName", StockLedger.ReportType } }
                )[0];

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_GroupMaster", StockLedger.GroupMaster);
                parameters.Add("p_SubGroupMaster", StockLedger.SubGroupMaster);
                parameters.Add("p_ItemMaster_Code", StockLedger.ItemMaster_Code);
                parameters.Add("p_FromDate", StockLedger.FromDate);
                parameters.Add("p_ToDate", StockLedger.ToDate);
                parameters.Add("p_ReportType", StockLedger.ReportType);
                var result = await conn.QueryAsync<dynamic>(data.Rows[0]["ProcedureName"].ToString(), parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetStockAuditMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string FromDate, string ToDate, string Mode)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", Mode.Trim());
                parameters.Add("p_FromDate", FromDate);
                parameters.Add("p_ToDate", ToDate);
                var result = await conn.QueryAsync<dynamic>("USP_StockAuditReport", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<VM_UPIID_Details> GetUPIIDReport(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,string UPI_ID)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var objMaster = new VM_UPIID_Details();

                var parameters = new Dictionary<string, object>
                {
                    { "p_UPI_ID", UPI_ID }
                };

                var TY_STRUCTUREArry = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                    _bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "USP_GetUPIIDDetails",
                    parameters,
                    CommandType.StoredProcedure);

                if (TY_STRUCTUREArry.Count > 0)
                {
                    objMaster.DispatchMaster = CommonFunctions.DatatableToDynamicList(TY_STRUCTUREArry[0]);
                    objMaster.SalesReturn = CommonFunctions.DatatableToDynamicList(TY_STRUCTUREArry[1]);
                    objMaster.MRNMaster = CommonFunctions.DatatableToDynamicList(TY_STRUCTUREArry[2]);
                }
                return objMaster;
            }
        }
        public async Task<IEnumerable<dynamic>> SaveGoldenCruiserQRDetails(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblGoldenCruiserQRDetails GoldenCruiserQRDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "INSERT");
                parameters.Add("p_ScanNo", GoldenCruiserQRDetails.ScanNo);
                var result = await conn.QueryAsync<dynamic>("USP_GoldenCruiserQRDetails", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetGoldenCruiserQRDetails(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode","LOCATE");
                parameters.Add("p_ScanNo", "");
                var result = await conn.QueryAsync<dynamic>("USP_GoldenCruiserQRDetails", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> ResetGoldenCruiserQRDetails(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "RESET");
                parameters.Add("p_ScanNo", "");
                var result = await conn.QueryAsync<dynamic>("USP_GoldenCruiserQRDetails", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
