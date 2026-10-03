using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class RetailerReplacementInService : IRetailerReplacementIn
    {
        string sp_name = "USP_RetailerReplacementIn";

        public async Task<IEnumerable<dynamic>> ShowRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "[]");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<VM_RetailerReplacementInList> ShowRetailerReplacementInByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            VM_RetailerReplacementInList model = new VM_RetailerReplacementInList();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWDATA" },
                { "@p_Code", code },
                { "@p_UserMaster_Code", 0 },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "[]" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_RetailerReplacementIn(@p_Mode,@p_Code,@p_UserMaster_Code,@p_jsonData,@p_jsonData1)",
                parameters,
                CommandType.Text
            ));
            model.RetailerReplacementIn = dataTables.Count > 0 ? CommonFunctions.DatatableToDynamicList(dataTables[0]) : [];
            model.RetailerReplacementInDetail = dataTables.Count > 1 ? CommonFunctions.DatatableToDynamicList(dataTables[1]) : [];
            return model;
        }

        public async Task<dynamic> SaveRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_RetailerReplacementIn model)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var header = model?.RetailerReplacementIn?.FirstOrDefault();
                var json = new JavaScriptSerializer().Serialize(model?.RetailerReplacementIn ?? []);
                var json1 = new JavaScriptSerializer().Serialize(model?.RetailerReplacementInDetail ?? []);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_Code", header?.Code ?? 0);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> DeleteRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "[]");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetBrandMasterListForReplacement(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "BRANDLIST");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "[]");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
