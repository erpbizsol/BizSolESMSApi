using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class PaymentEntryService: IPaymentEntry
    {
        string sp_name = "USP_BillMaster";
        string sp_get_name = "USP_GetInvoiceBillDetails";
        string sp_pending_invoice_report = "USP_GetPendingInvoiceReport";

        public async Task<IEnumerable<dynamic>> GetInvoiceDetailsByAccountMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int AccountMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", "");
                parameters.Add("p_Mode", "ByAccount");
                parameters.Add("p_AccountMaster_Code", AccountMaster_Code);
                parameters.Add("p_FromDate", "");
                parameters.Add("p_ToDate", "");
                parameters.Add("p_PaymentMode", "");

                var result = await conn.QueryAsync<dynamic>(sp_get_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<dynamic> SavePaymentEntry(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, VM_BillMaster vmBillMaster, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmBillMaster.BillMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmBillMaster.BillAdjustmentDetails);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", vmBillMaster.BillMaster.FirstOrDefault()?.Code ?? 0);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetPaymentMasterlist(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string FromDate, string ToDate, int AccountMaster_Code, string PaymentMode)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", "");
                parameters.Add("p_Mode", "Locate");
                parameters.Add("p_AccountMaster_Code", AccountMaster_Code);
                parameters.Add("p_FromDate", FromDate);
                parameters.Add("p_ToDate", ToDate);
                parameters.Add("p_PaymentMode", PaymentMode);

                var result = await conn.QueryAsync<dynamic>(sp_get_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<VM_BillMasterList> GetPaymentEntryByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code)
        {
            VM_BillMasterList vM_BillMasterList = new VM_BillMasterList();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Code", Code.ToString() },
                { "@p_Mode", "EDIT" },
                { "@p_AccountMaster_Code", 0 },
                { "@p_FromDate", "" },
                { "@p_ToDate", "" },
                { "@p_PaymentMode", "" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                _bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_GetInvoiceBillDetails(@p_Code,@p_Mode,@p_AccountMaster_Code,@p_FromDate,@p_ToDate,@p_PaymentMode)",
                parameters,
                CommandType.Text
            ));

            vM_BillMasterList.BillMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_BillMasterList.BillAdjustmentDetails = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_BillMasterList;
        }

        public async Task<dynamic> DeletePaymentEntry(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code.ToString());
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_AccountMaster_Code", 0);
                parameters.Add("p_FromDate", "");
                parameters.Add("p_ToDate", "");
                parameters.Add("p_PaymentMode", "");

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_get_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetPendingInvoiceReport(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int AccountMaster_Code, string AsonDate)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_AccountMaster_Code", AccountMaster_Code);
                parameters.Add("p_AsonDate", AsonDate);

                var result = await conn.QueryAsync<dynamic>(sp_pending_invoice_report, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<dynamic> SavePaymentEntryAdjustment(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, VM_BillMaster vmBillMaster, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmBillMaster.BillMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmBillMaster.BillAdjustmentDetails);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", vmBillMaster.BillMaster.FirstOrDefault()?.Code ?? 0);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_BillAdjustmentlMaster", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetPaymentAdjustmentMasterlist(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string FromDate, string ToDate, int AccountMaster_Code, string PaymentMode)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", "");
                parameters.Add("p_Mode", "Locate");
                parameters.Add("p_AccountMaster_Code", AccountMaster_Code);
                parameters.Add("p_FromDate", FromDate);
                parameters.Add("p_ToDate", ToDate);
                parameters.Add("p_PaymentMode", PaymentMode);

                var result = await conn.QueryAsync<dynamic>("USP_GetInvoiceBillAdjustmentDetails", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<VM_BillMasterList> GetPaymentEntryAdjustmentByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code)
        {
            VM_BillMasterList vM_BillMasterList = new VM_BillMasterList();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Code", Code.ToString() },
                { "@p_Mode", "EDIT" },
                { "@p_AccountMaster_Code", 0 },
                { "@p_FromDate", "" },
                { "@p_ToDate", "" },
                { "@p_PaymentMode", "" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                _bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_GetInvoiceBillAdjustmentDetails(@p_Code,@p_Mode,@p_AccountMaster_Code,@p_FromDate,@p_ToDate,@p_PaymentMode)",
                parameters,
                CommandType.Text
            ));

            vM_BillMasterList.BillMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_BillMasterList.BillAdjustmentDetails = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_BillMasterList;
        }

        public async Task<dynamic> DeletePaymentEntryAdjustment(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code.ToString());
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_AccountMaster_Code", 0);
                parameters.Add("p_FromDate", "");
                parameters.Add("p_ToDate", "");
                parameters.Add("p_PaymentMode", "");

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_GetInvoiceBillAdjustmentDetails", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> GetPaymentModeList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "PaymentMode");
                parameters.Add("p_AccountMaster_Code", 0);
                parameters.Add("p_FromDate", "");
                parameters.Add("p_ToDate", "");
                parameters.Add("p_PaymentMode", "");

                var result = await conn.QueryAsync<dynamic>("USP_GetInvoiceBillDetails", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
