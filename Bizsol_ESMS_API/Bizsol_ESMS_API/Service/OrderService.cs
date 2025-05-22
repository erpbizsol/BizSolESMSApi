using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using Microsoft.Graph.Models.IdentityGovernance;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class OrderService:IOrder
    {
     
        string sp_name = "USP_OrderMaster";
        public async Task<IEnumerable<dynamic>> ShowOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", "");
                parameters.Add("p_ItemName", "");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<VM_OrderMasterForShow> ShowOrderMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
          
            VM_OrderMasterForShow vM_OrderMaster = new VM_OrderMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWDATA" },
                { "@p_UserMaster_Code", 0 },
                { "@p_Code", Code },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" },
                { "@p_AccountName", "" },
                { "@p_ItemName", "" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_OrderMaster(@p_Mode,@p_UserMaster_Code,@p_Code,@p_jsonData, @p_jsonData1,@p_AccountName,@p_ItemName)",
                    parameters,
                    CommandType.Text
                ));
            vM_OrderMaster.OrderMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_OrderMaster.OrderDetial = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_OrderMaster;
        }
        public async Task<dynamic> DeleteOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", "");
                parameters.Add("p_ItemName", "");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> InsertOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_OrderMaster vmOrderMaster, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmOrderMaster.OrderMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmOrderMaster.OrderDetial);
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code", vmOrderMaster.OrderMaster.FirstOrDefault().Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);
                parameters.Add("p_AccountName", "");
                parameters.Add("p_ItemName", "");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> ClientWiseRate(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string ClientName, string ItemName)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "VenderWiseRate");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", ClientName);
                parameters.Add("p_ItemName", ItemName);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<dynamic> ImportOrder(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportOrder ImportOrder)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(ImportOrder.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_AccountName", ImportOrder.ClientName);
                parameters.Add("p_ClientType", ImportOrder.ClientType);
                parameters.Add("p_OrderNo", ImportOrder.OrderNo);
                parameters.Add("p_UserMaster_Code", ImportOrder.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("UDF_ImportOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportOrderForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportOrder ImportOrder)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(ImportOrder.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "GET");
                parameters.Add("p_AccountName", ImportOrder.ClientName);
                parameters.Add("p_ClientType", ImportOrder.ClientType);
                parameters.Add("p_OrderNo", ImportOrder.OrderNo);
                parameters.Add("p_UserMaster_Code", ImportOrder.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("UDF_ImportOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> ShowBoxNumber(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string PickListNo)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                string sp_name1 = "USP_BoxNo";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_PickListNo", PickListNo);
                var result = await conn.QueryAsync<dynamic>(sp_name1, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetEmployeeList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int UserMaster_Code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                string sp_name1 = "USP_UserList";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>(sp_name1, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<dynamic> ImportSalesReturn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblSalesReturn SalesReturn)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(SalesReturn.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_AccountMaster_Code", SalesReturn.ClientMaster_Code);
                parameters.Add("p_ReasonMaster_Code", SalesReturn.ReasonMaster_Code);
                parameters.Add("p_OrderNo", SalesReturn.OrderNo);
                parameters.Add("p_UserMaster_Code", SalesReturn.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("UDF_ImportSalesReturn", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportSalesReturnForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblSalesReturn SalesReturn)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(SalesReturn.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "GET");
                parameters.Add("p_AccountMaster_Code", SalesReturn.ClientMaster_Code);
                parameters.Add("p_ReasonMaster_Code", SalesReturn.ReasonMaster_Code);
                parameters.Add("p_OrderNo", SalesReturn.OrderNo);
                parameters.Add("p_UserMaster_Code", SalesReturn.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("UDF_ImportSalesReturn", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> ShowSalesReturnMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_ReasonMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>("USP_SalesReturnMaster", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<IEnumerable<dynamic>> ShowSalesReturnMasterDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code",Code);
                parameters.Add("p_Mode", "GETSALERETURNITEM");
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_ReasonMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>("USP_SalesReturnMaster", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> SaveSalesReturnScanDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblScanSalesReturn SalesReturn)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SCAN");
                parameters.Add("p_Code", SalesReturn.Code);
                parameters.Add("p_ScanNo", SalesReturn.ScanNo);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_SaveScanSalesReturn", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> UpdateReasonMaster_CodeInSaleReturn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,int Code,int ReasonMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "UPDATEREASON");
                parameters.Add("p_Code",Code);
                parameters.Add("p_UserMaster_Code",0);
                parameters.Add("p_ReasonMaster_Code", ReasonMaster_Code);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_SalesReturnMaster", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportOpeningBalance(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportOpeningBalance OpeningBalance)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(OpeningBalance.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_WarehouseName", OpeningBalance.WarehouseName);
                parameters.Add("p_UserMaster_Code", OpeningBalance.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_ImportItemOpeningBalance", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportOpeningBalanceForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportOpeningBalance OpeningBalance)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(OpeningBalance.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GET");
                parameters.Add("p_WarehouseName", OpeningBalance.WarehouseName);
                parameters.Add("p_UserMaster_Code", OpeningBalance.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("USP_ImportItemOpeningBalance", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetStockAuditList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_ScanNo", "");
                parameters.Add("p_UserMaster_Code",0);
                var result = await conn.QueryAsync<dynamic>("USP_GetStockAuditDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> ScanStockAudit(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblStockAudit StockAudit)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "SCAN");
                parameters.Add("p_ScanNo",StockAudit.ScanNo);
                parameters.Add("p_UserMaster_Code", StockAudit.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_GetStockAuditDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> ManualStockAudit(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,int Code,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "COMPLETE");
                parameters.Add("p_ScanNo", "");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_GetStockAuditDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> ImportTATReport(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblTATReport TATReport)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(TATReport.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_TATDate", TATReport.TATDate);
                parameters.Add("p_IsCheck", TATReport.IsCheck);
                parameters.Add("p_UserMaster_Code", TATReport.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_ImportTATReport", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportTATReportForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblTATReport TATReport)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(TATReport.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GET");
                parameters.Add("p_TATDate", TATReport.TATDate);
                parameters.Add("p_IsCheck", TATReport.IsCheck);
                parameters.Add("p_UserMaster_Code", TATReport.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("USP_ImportTATReport", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetTATReportList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string TATDate,string Type)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", Type.Trim());
                parameters.Add("p_Code", 0);
                parameters.Add("p_POD", "");
                parameters.Add("p_VehicleNo", "");
                parameters.Add("p_Redispatch", "");
                parameters.Add("p_Remark", "");
                parameters.Add("p_TATDate", TATDate);
                parameters.Add("p_UserMaster_Code",0);
                var result = await conn.QueryAsync<dynamic>("USP_GetTATReport", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> SaveTATDetails(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblSaveTATMaster TATMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_Code", TATMaster.Code);
                parameters.Add("p_POD", TATMaster.POD);
                parameters.Add("p_VehicleNo", TATMaster.VehicleNo.Trim());
                parameters.Add("p_Redispatch", TATMaster.Redispatch);
                parameters.Add("p_Remark", TATMaster.Remark.Trim());
                parameters.Add("p_TATDate","");
                parameters.Add("p_UserMaster_Code", TATMaster.UserMaster_Code);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_GetTATReport", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
