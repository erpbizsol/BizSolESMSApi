using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class RetailerReplacementOutService : IRetailerReplacementOut
    {
        string sp_name = "USP_RetailerReplacementOut";

        public async Task<IEnumerable<dynamic>> ShowRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
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

        public async Task<VM_RetailerReplacementOutList> ShowRetailerReplacementOutByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            return await ShowTwoResultSets(bizsolESMSConnectionDetails, "SHOWDATA", code);
        }

        public async Task<VM_RetailerReplacementOutList> ShowDataByEntryNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            return await ShowTwoResultSets(bizsolESMSConnectionDetails, "SHOWBYENTRY", code);
        }

        public async Task<dynamic> SaveRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_RetailerReplacementOut model)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var header = model?.RetailerReplacementOut?.FirstOrDefault();
                var json = new JavaScriptSerializer().Serialize(model?.RetailerReplacementOut ?? []);
                var json1 = new JavaScriptSerializer().Serialize(model?.RetailerReplacementOutDetail ?? []);
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

        public async Task<dynamic> DeleteRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
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

        public async Task<IEnumerable<dynamic>> GetEntryNoList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            return await ShowList(bizsolESMSConnectionDetails, "ENTRYLIST");
        }

        public async Task<IEnumerable<dynamic>> GetBrandMasterListForReplacement(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            return await ShowList(bizsolESMSConnectionDetails, "BRANDLIST");
        }

        private async Task<IEnumerable<dynamic>> ShowList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string mode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", mode);
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "[]");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        private async Task<VM_RetailerReplacementOutList> ShowTwoResultSets(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string mode, int code)
        {
            VM_RetailerReplacementOutList model = new VM_RetailerReplacementOutList();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", mode },
                { "@p_Code", code },
                { "@p_UserMaster_Code", 0 },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "[]" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_RetailerReplacementOut(@p_Mode,@p_Code,@p_UserMaster_Code,@p_jsonData,@p_jsonData1)",
                parameters,
                CommandType.Text
            ));
            model.RetailerReplacementOut = dataTables.Count > 0 ? CommonFunctions.DatatableToDynamicList(dataTables[0]) : [];
            model.RetailerReplacementOutDetail = dataTables.Count > 1 ? CommonFunctions.DatatableToDynamicList(dataTables[1]) : [];
            return model;
        }
    }
}
