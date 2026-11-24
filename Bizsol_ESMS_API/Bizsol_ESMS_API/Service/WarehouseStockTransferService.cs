// Program.cs me register karna:
//     builder.Services.AddScoped<IWarehouseStockTransferService, WarehouseStockTransferService>();
using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySqlConnector;
using System.Data;
using System.Globalization;

namespace Bizsol_ESMS_API.Services
{
    public class WarehouseStockTransferService : IWarehouseStockTransferService
    {
        // Saare modes (SCAN, LIST, DETAILS, UPDATEHEADER) isi ek proc me hain
        string sp_name = "USP_WarehouseStockTransferMaster";

        public async Task<IEnumerable<dynamic>> GetList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string? fromDate, string? toDate, int warehouseMasterCode, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("LIST", UserMaster_Code, 0);
                parameters.Add("p_FromDate", ParseDate(fromDate), DbType.Date);
                parameters.Add("p_ToDate", ParseDate(toDate), DbType.Date);
                parameters.Add("p_WarehouseMaster_Code", warehouseMasterCode);
                return await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<WarehouseTransferDetailResponse> GetDetails(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("DETAILS", UserMaster_Code, code);
                using (var multi = await conn.QueryMultipleAsync(sp_name, parameters, commandType: CommandType.StoredProcedure))
                {
                    var master = await multi.ReadAsync<dynamic>();
                    var detail = multi.IsConsumed ? new List<dynamic>() : await multi.ReadAsync<dynamic>();
                    return new WarehouseTransferDetailResponse
                    {
                        Master = master.ToList(),
                        Detail = detail.ToList()
                    };
                }
            }
        }

        public async Task<IEnumerable<dynamic>> ScanItem(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, WarehouseTransferScanModel model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("SCAN", model.UserMaster_Code, model.Code);
                parameters.Add("p_ScanNo", model.ScanNo ?? "");
                parameters.Add("p_EntryDate", ParseDate(model.EntryDate), DbType.Date);
                parameters.Add("p_FromWarehouseMaster_Code", model.FromWarehouseMaster_Code);
                parameters.Add("p_ToWarehouseMaster_Code", model.ToWarehouseMaster_Code);
                parameters.Add("p_RefNo", model.RefNo ?? "");
                return await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> UpdateHeader(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, WarehouseTransferHeaderModel model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("UPDATEHEADER", model.UserMaster_Code, model.Code);
                parameters.Add("p_EntryDate", ParseDate(model.EntryDate), DbType.Date);
                parameters.Add("p_RefNo", model.RefNo ?? "");
                return await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> CompleteTransfer(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, WarehouseTransferCompleteModel model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("COMPLETE", model.UserMaster_Code, model.Code);
                return await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<dynamic>> DeleteTransfer(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, WarehouseTransferCompleteModel model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("DELETE", model.UserMaster_Code, model.Code);
                return await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<WarehouseTransferPrintResponse> GetPrintData(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = BuildMainParams("PRINT", UserMaster_Code, code);
                using (var multi = await conn.QueryMultipleAsync(sp_name, parameters, commandType: CommandType.StoredProcedure))
                {
                    var header = await multi.ReadAsync<dynamic>();
                    var items = multi.IsConsumed ? new List<dynamic>() : await multi.ReadAsync<dynamic>();
                    return new WarehouseTransferPrintResponse
                    {
                        Header = header.ToList(),
                        Items = items.ToList()
                    };
                }
            }
        }
        private static DynamicParameters BuildMainParams(string mode, int UserMaster_Code, int code)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("p_Mode", mode);
            parameters.Add("p_UserMaster_Code", UserMaster_Code);
            parameters.Add("p_Code", code);
            parameters.Add("p_FromDate", null, DbType.Date);
            parameters.Add("p_ToDate", null, DbType.Date);
            parameters.Add("p_WarehouseMaster_Code", 0);
            parameters.Add("p_EntryDate", null, DbType.Date);
            parameters.Add("p_RefNo", "");
            parameters.Add("p_ScanNo", "");
            parameters.Add("p_FromWarehouseMaster_Code", 0);
            parameters.Add("p_ToWarehouseMaster_Code", 0);
            return parameters;
        }

        private static DateTime? ParseDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string[] formats = { "yyyy-MM-dd", "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-ddTHH:mm:ss" };
            return DateTime.TryParseExact(value.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                ? dt.Date
                : null;
        }
    }
}
