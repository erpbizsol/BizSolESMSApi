using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class OrderCancellationService : IOrderCancellation
    {
        private const string SpName = "USP_OrderCancellation";

        private static DynamicParameters BuildParameters(
            string mode,
            int code,
            int userMasterCode = 0,
            int reasonMasterCode = 0,
            string remark = "",
            string jsonData = "{}")
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("p_Code", code);
            parameters.Add("p_Mode", mode);
            parameters.Add("p_UserMaster_Code", userMasterCode);
            parameters.Add("p_ReasonMaster_Code", reasonMasterCode);
            parameters.Add("p_Remark", remark ?? "");
            parameters.Add("p_jsonData", jsonData);
            return parameters;
        }

        public async Task<IEnumerable<dynamic>> GetOrderCancellationList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("LOCATE", 0, bizsolESMSConnectionDetails.UserMaster_Code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetOrderCancellationLines(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("GETLINES", code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetOrderCancellationHeader(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("GETHEADER", code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetOrderCancellationItems(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("GETITEMS", code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<VM_OrderCancellationDetail> GetOrderCancellationDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            VM_OrderCancellationDetail detail = new VM_OrderCancellationDetail();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Code", code },
                { "@p_Mode", "GETDETAIL" },
                { "@p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code },
                { "@p_ReasonMaster_Code", 0 },
                { "@p_Remark", "" },
                { "@p_jsonData", "{}" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_OrderCancellation(@p_Code, @p_Mode, @p_UserMaster_Code, @p_ReasonMaster_Code, @p_Remark, @p_jsonData)",
                parameters,
                CommandType.Text));

            detail.OrderMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            detail.OrderDetial = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return detail;
        }

        public async Task<IEnumerable<dynamic>> GetOrderCancellationDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("GETDISPATCH", code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<dynamic> SaveOrderCancellation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblOrderCancellationSave model, int userMasterCode)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var json = new JavaScriptSerializer().Serialize(new
            {
                model.OrderMaster_Code,
                model.ReasonMaster_Code,
                model.Remark,
                model.Details
            });

            var parameters = BuildParameters(
                "SAVE",
                model.OrderMaster_Code,
                userMasterCode,
                model.ReasonMaster_Code,
                model.Remark ?? "",
                json);

            var result = await conn.QueryFirstOrDefaultAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<dynamic>> GetReOpenOrderList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("REOPEN_LIST", 0, bizsolESMSConnectionDetails.UserMaster_Code);
            var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<dynamic> ReOpenOrderCancellation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code, int userMasterCode)
        {
            using IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp);
            var parameters = BuildParameters("REOPEN", code, userMasterCode);
            var result = await conn.QueryFirstOrDefaultAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
