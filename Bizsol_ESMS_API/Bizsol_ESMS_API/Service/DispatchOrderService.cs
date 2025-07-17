using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class DispatchOrderService:IDispatchOrder
    {
        string sp_name = "USP_DispatchOrder";
        public async Task<IEnumerable<dynamic>> GetDispatchOrderList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
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
                parameters.Add("p_OrderNoWithPrefix", "");

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<VM_ShowDispatchOrder> GetDispatchOrderByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            VM_ShowDispatchOrder vM_DispatchOrder = new VM_ShowDispatchOrder();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWDATA" },
                { "@p_UserMaster_Code", 0 },
                { "@p_Code",Code},
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" },
                { "@p_AccountName", "" },
                { "@p_ItemName", "" },

            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_DispatchOrder(@p_Mode,@p_UserMaster_Code,@p_Code,@p_jsonData, @p_jsonData1,@p_AccountName,@p_ItemName)",
                    parameters,
                    CommandType.Text
                ));
            vM_DispatchOrder.DispatchMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_DispatchOrder.DispatchDetails = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_DispatchOrder;
        }
        public async Task<dynamic> SaveDispatchOrderEntry(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_SaveDispatchOrder vmDispatchOrder, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmDispatchOrder.DispatchMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmDispatchOrder.DispatchDetails);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", vmDispatchOrder.DispatchMaster.FirstOrDefault().Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);
                parameters.Add("p_AccountName", 0);
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", "");


                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteDispatchOrder(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", 0);
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", "");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetClientWiseOrderNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string ClientName)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "GETORDERNO");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", ClientName.Trim());
                parameters.Add("p_ItemName","");
                parameters.Add("p_OrderNoWithPrefix", "");

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetItemDetailByOrderNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string OrderNo)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "GETITEMDETAIL");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName","");
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", OrderNo);

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetClientWiseShowOrder(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string Mode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", Mode.Trim());
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName","");
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", "");

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<VM_OrderMasterForShow> GetOrderDetailsForDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code,string Mode,int DispatchMaster_Code)
        {

            VM_OrderMasterForShow vM_OrderMaster = new VM_OrderMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", Mode.Trim()},
                { "@p_DispatchMaster_Code", DispatchMaster_Code},
                { "@p_Code", Code }
            };
            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_ModeWiseDispatch(@p_Mode,@p_DispatchMaster_Code,@p_Code)",
                    parameters,
                    CommandType.Text
                ));
            vM_OrderMaster.OrderMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_OrderMaster.OrderDetial = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_OrderMaster;
        }
        public async Task<dynamic> ScanItemForDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblScanDispatch Dispatch, string Mode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", Mode);
                parameters.Add("p_Code", Dispatch.Code);
                parameters.Add("p_ScanNo", Dispatch.ScanNo);
                parameters.Add("p_UserMaster_Code", Dispatch.UserMaster_Code);
                parameters.Add("p_ScanQty", Dispatch.ScanQty);
                parameters.Add("p_ManualQty", Dispatch.ManualQty);
                parameters.Add("p_DispatchQty", Dispatch.DispatchQty);
                parameters.Add("p_DispatchMaster_Code", Dispatch.DispatchMaster_Code);
                parameters.Add("p_packedBy", Dispatch.PackedBy);
                parameters.Add("p_BoxNo", Dispatch.BoxNo);
                var result = await conn.QueryAsync<dynamic>("USP_SaveScanDispatchOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ManualItemForDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblScanDispatch Dispatch,string Mode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", Mode);
                parameters.Add("p_Code", Dispatch.Code);
                parameters.Add("p_ScanNo", Dispatch.ScanNo);
                parameters.Add("p_UserMaster_Code", Dispatch.UserMaster_Code);
                parameters.Add("p_ScanQty", Dispatch.ScanQty);
                parameters.Add("p_ManualQty", Dispatch.ManualQty);
                parameters.Add("p_DispatchQty", Dispatch.DispatchQty);
                parameters.Add("p_DispatchMaster_Code", Dispatch.DispatchMaster_Code);
                parameters.Add("p_packedBy", Dispatch.PackedBy);
                var result = await conn.QueryAsync<dynamic>("USP_SaveManualDispatchOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            } 
        }
        public async Task<dynamic> GetMarkasCompeteByOrderNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GETCode");
                parameters.Add("p_Code", Code);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", "{}");
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", "");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetDispatchReport(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "DISPATCHREPORT");
                parameters.Add("p_Code", Code);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", "");
                parameters.Add("p_ItemName", "");
                parameters.Add("p_OrderNoWithPrefix", "");

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> UpdateBoxNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblUpdateBoxNo UpdateBoxNo, string Mode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", Mode);
                parameters.Add("p_Code", UpdateBoxNo.Code);
                parameters.Add("p_ScanNo", UpdateBoxNo.ScanNo);
                parameters.Add("p_BoxNo", UpdateBoxNo.BoxNo);
                var result = await conn.QueryAsync<dynamic>("USP_UpdateBoxNo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteDispatchItemQty(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", Code);
                parameters.Add("p_ScanNo", "");
                parameters.Add("p_BoxNo", 0);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_UpdateBoxNo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> SaveManualRateAndQty(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblManualRateAndQty Dispatch)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_UserMaster_Code", Dispatch.UserMaster_Code);
                parameters.Add("p_ManualQty", Dispatch.ManualQty);
                parameters.Add("p_DispatchMaster_Code", Dispatch.DispatchMaster_Code);
                parameters.Add("p_BoxNo", Dispatch.BoxNo);
                parameters.Add("p_ItemCode", Dispatch.ItemCode.Trim());
                parameters.Add("p_OrderMaster_Code", Dispatch.OrderMaster_Code);
                parameters.Add("p_MRP", Dispatch.MRP);
                var result = await conn.QueryAsync<dynamic>("USP_SaveManualMRPANDQTY", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetDispatchQRDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code", Code);

                var result = await conn.QueryAsync<dynamic>("USP_DispatchQRDetail", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetDispatchValidationDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode","LOCATE");
                parameters.Add("p_Code",0);
                parameters.Add("p_BoxNo", "");
                parameters.Add("p_VehicleNo", "");
                parameters.Add("p_InvoiceNo", "");
                parameters.Add("p_DriverName", "");
                parameters.Add("p_DriverContactNo", "");
                parameters.Add("p_LorryMeter", 0);
                parameters.Add("p_UserMaster_Code", 0);

                var result = await conn.QueryAsync<dynamic>("USP_DispatchBoxValidation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetDispatchValidationViewData(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "VIEW");
                parameters.Add("p_Code", Code);
                parameters.Add("p_BoxNo", "");
                parameters.Add("p_VehicleNo","");
                parameters.Add("p_InvoiceNo","");
                parameters.Add("p_DriverName","");
                parameters.Add("p_DriverContactNo","");
                parameters.Add("p_LorryMeter",0);
                parameters.Add("p_UserMaster_Code",0);

                var result = await conn.QueryAsync<dynamic>("USP_DispatchBoxValidation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> SaveDispatchBoxValidation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblDispatchBoxValidation DispatchBoxValidation)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SCAN");
                parameters.Add("p_Code", DispatchBoxValidation.Code);
                parameters.Add("p_BoxNo", DispatchBoxValidation.BoxNo);
                parameters.Add("p_VehicleNo", DispatchBoxValidation.VehicleNo);
                parameters.Add("p_InvoiceNo", DispatchBoxValidation.InvoiceNo);
                parameters.Add("p_DriverName", DispatchBoxValidation.DriverName);
                parameters.Add("p_DriverContactNo", DispatchBoxValidation.DriverContactNo);
                parameters.Add("p_LorryMeter", DispatchBoxValidation.LorryMeter);
                parameters.Add("p_UserMaster_Code", DispatchBoxValidation.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_DispatchBoxValidation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetDispatchValidationEditData(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "EDIT");
                parameters.Add("p_Code", Code);
                parameters.Add("p_BoxNo", "");
                parameters.Add("p_VehicleNo", "");
                parameters.Add("p_InvoiceNo", "");
                parameters.Add("p_DriverName", "");
                parameters.Add("p_DriverContactNo", "");
                parameters.Add("p_LorryMeter", 0);
                parameters.Add("p_UserMaster_Code", 0);

                var result = await conn.QueryAsync<dynamic>("USP_DispatchBoxValidation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetGatePassData(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string VehicleNo,string Date)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "SHOWDATA");
                parameters.Add("p_VehicleNo", VehicleNo.Trim());
                parameters.Add("p_Date", Date);
                parameters.Add("p_Codes", "");

                var result = await conn.QueryAsync<dynamic>("USP_PrintGatePass", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetVehicleNoForDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "VEHICLENO");
                parameters.Add("p_Code", 0);
                parameters.Add("p_BoxNo", "");
                parameters.Add("p_VehicleNo", "");
                parameters.Add("p_InvoiceNo", "");
                parameters.Add("p_DriverName", "");
                parameters.Add("p_DriverContactNo", "");
                parameters.Add("p_LorryMeter", 0);
                parameters.Add("p_UserMaster_Code", 0);

                var result = await conn.QueryAsync<dynamic>("USP_DispatchBoxValidation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetOrderPackedDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string Date)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Date", Date);

                var result = await conn.QueryAsync<dynamic>("USP_GetOrderPackedDetail", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
