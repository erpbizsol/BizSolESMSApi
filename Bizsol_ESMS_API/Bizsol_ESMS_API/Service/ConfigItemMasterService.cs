using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class ConfigItemMasterService :IConfigItemMaster
    {
        string sp_name = "USP_ConfigItemMaster";
        public async Task<spOutputParameter> SaveConfig(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblConfigItemMaster model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "INSERT");
                parameters.Add("p_Code", model.Code);
                parameters.Add("p_ItemNameHeader", model.ItemNameHeader);
                parameters.Add("p_ItembarcodeHeader", model.ItembarcodeHeader);
                parameters.Add("p_GroupItemHeader", model.GroupItemHeader);
                parameters.Add("p_SubGroupItemHeader", model.SubGroupItemHeader);
                parameters.Add("p_LocationItemHeader", model.LocationItemHeader);
                parameters.Add("p_ItemCode", model.ItemCode);
                parameters.Add("p_ItemCodeHeader", model.ItemCodeHeader);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);
                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }

        public async Task<IEnumerable<dynamic>> ShowItemConfig(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_ItemNameHeader", null);
                parameters.Add("p_ItembarcodeHeader", null);
                parameters.Add("p_GroupItemHeader", null);
                parameters.Add("p_SubGroupItemHeader", null);
                parameters.Add("p_LocationItemHeader", null);
                parameters.Add("p_ItemCode", null);
                parameters.Add("p_ItemCodeHeader","");
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
    }
}
