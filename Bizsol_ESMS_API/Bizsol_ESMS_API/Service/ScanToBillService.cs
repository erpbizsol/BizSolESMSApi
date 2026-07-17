using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using Microsoft.AspNetCore.Components;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class ScanToBillService : IScanToBill
    {
        public async Task<dynamic> SaveItemScanToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblScanToBill Dispatch)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode","SCAN");
                parameters.Add("p_Code", Dispatch.Code);
                parameters.Add("p_OrderNo", Dispatch.InvoiceNo);
                parameters.Add("p_ScanNo", Dispatch.ScanNo);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_packedBy", Dispatch.PackedBy);
                parameters.Add("p_ClientName", Dispatch.AccountName);
                parameters.Add("p_BoxNo", Dispatch.BoxNo);
                parameters.Add("p_WarehouseMaster_Code", Dispatch.WarehouseMaster_Code);
                parameters.Add("p_IsManual", Dispatch.IsManual);
                var result = await conn.QueryAsync<dynamic>("USP_SaveScanAndBillOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> ManuaItemScanToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblScanDispatch Dispatch)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Dispatch.Code);
                parameters.Add("p_ScanNo", Dispatch.ScanNo);
                parameters.Add("p_UserMaster_Code", Dispatch.UserMaster_Code);
                parameters.Add("p_ScanQty", Dispatch.ScanQty);
                parameters.Add("p_ManualQty", Dispatch.ManualQty);
                parameters.Add("p_DispatchQty", Dispatch.DispatchQty);
                parameters.Add("p_DispatchMaster_Code", Dispatch.DispatchMaster_Code);
                parameters.Add("p_packedBy", Dispatch.PackedBy);
                var result = await conn.QueryAsync<dynamic>("USP_SaveManualScanAndBillOrder", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<VM_OrderMasterForShow> GetDetailsItemScanToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            VM_OrderMasterForShow vM_OrderMaster = new VM_OrderMasterForShow();

            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Details");
                parameters.Add("p_Code", Code);

                using var multi = await conn.QueryMultipleAsync(
                    "call USP_ScanAndBillOrder(@p_Mode,@p_Code)",
                    parameters,
                    commandType: CommandType.Text);

                vM_OrderMaster.OrderMaster = (await multi.ReadAsync<dynamic>()).ToList();
                vM_OrderMaster.OrderDetial = (await multi.ReadAsync<dynamic>()).ToList();
            }
            return vM_OrderMaster;
        }
        public async Task<dynamic> AddItemScanToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblAddItemScanToBill AddItemScanToBill)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "AddItem");
                parameters.Add("p_DispatchMaster_Code", AddItemScanToBill.DispatchMaster_Code);
                parameters.Add("p_ItemMaster_Code", AddItemScanToBill.ItemMaster_Code);
                parameters.Add("p_BoxNo", AddItemScanToBill.BoxNo);
                parameters.Add("p_ManualQty", AddItemScanToBill.ManualQty);
                parameters.Add("p_Mrp", AddItemScanToBill.Mrp);
                parameters.Add("p_OrderNo", AddItemScanToBill.OrderNo);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_packedBy", AddItemScanToBill.PackedBy);
                parameters.Add("p_ClientName", AddItemScanToBill.ClientName);
                parameters.Add("p_WarehouseMaster_Code", AddItemScanToBill.WarehouseMaster_Code);
                parameters.Add("p_IsManual", AddItemScanToBill.IsManual);
                parameters.Add("p_ItemCode", "");
                var result = await conn.QueryAsync<dynamic>("USP_AddItemForSacnToBill", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> SaveManualRateAndQtySacnToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblManualRateAndQty Dispatch)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Update");
                parameters.Add("p_DispatchMaster_Code", Dispatch.DispatchMaster_Code);
                parameters.Add("p_ItemMaster_Code", 0);
                parameters.Add("p_ItemCode", Dispatch.ItemCode);
                parameters.Add("p_BoxNo", Dispatch.BoxNo);
                parameters.Add("p_ManualQty", Dispatch.ManualQty);
                parameters.Add("p_Mrp", Dispatch.MRP);
                parameters.Add("p_OrderNo", "");
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_packedBy", "");
                parameters.Add("p_ClientName","");
                parameters.Add("p_WarehouseMaster_Code", 0);
                parameters.Add("p_IsManual","Y");
               
                var result = await conn.QueryAsync<dynamic>("USP_AddItemForSacnToBill", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteItemFormScanToBill(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Delete");
                parameters.Add("p_DispatchMaster_Code", Code);
                parameters.Add("p_ItemMaster_Code", 0);
                parameters.Add("p_BoxNo", 0);
                parameters.Add("p_ManualQty", 0);
                parameters.Add("p_Mrp",0);
                parameters.Add("p_OrderNo","");
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_packedBy", "");
                parameters.Add("p_ClientName", "");
                parameters.Add("p_WarehouseMaster_Code", 0);
                parameters.Add("p_IsManual", "");
                parameters.Add("p_ItemCode", "");
                var result = await conn.QueryAsync<dynamic>("USP_AddItemForSacnToBill", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
