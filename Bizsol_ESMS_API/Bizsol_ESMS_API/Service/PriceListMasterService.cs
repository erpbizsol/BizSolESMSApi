using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class PriceListMasterService : IPriceListMaster
    {
        string sp_name = "USP_PriceListMaster";

        public async Task<IEnumerable<dynamic>> ShowPriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_EffectiveDate", "");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<VM_PriceListMasterForShow> ShowPriceListMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code)
        {
            VM_PriceListMasterForShow vm = new VM_PriceListMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWDATA" },
                { "@p_Code", code },
                { "@p_UserMaster_Code", 0 },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" },
                { "@p_EffectiveDate", "" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_PriceListMaster(@p_Mode,@p_Code,@p_UserMaster_Code,@p_jsonData,@p_jsonData1,@p_EffectiveDate)",
                parameters,
                CommandType.Text));

            vm.PriceListMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vm.PriceListDetail = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vm;
        }

        public async Task<VM_PriceListMasterForShow> ShowPriceListMasterByEffectiveDate(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string effectiveDate)
        {
            VM_PriceListMasterForShow vm = new VM_PriceListMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWBYEFFECTIVEDATE" },
                { "@p_Code", 0 },
                { "@p_UserMaster_Code", 0 },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" },
                { "@p_EffectiveDate", effectiveDate ?? "" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                bizsolESMSConnectionDetails.DefultMysqlTemp,
                "call USP_PriceListMaster(@p_Mode,@p_Code,@p_UserMaster_Code,@p_jsonData,@p_jsonData1,@p_EffectiveDate)",
                parameters,
                CommandType.Text));

            vm.PriceListMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vm.PriceListDetail = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vm;
        }

        public async Task<dynamic> ValidatePriceListEffectiveDate(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string effectiveDate)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "VALIDATEEFFECTIVEDATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_EffectiveDate", effectiveDate ?? "");
                return await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> SavePriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_PriceListMaster model, int userMasterCode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_Code", model.PriceListMaster.First().Code);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", JsonConvert.SerializeObject(model.PriceListMaster));
                parameters.Add("p_jsonData1", JsonConvert.SerializeObject(model.PriceListDetail));
                parameters.Add("p_EffectiveDate", "");
                return await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> DeletePriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code, int userMasterCode)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_EffectiveDate", "");
                return await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
