using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class SubGroupMasterService:ISubGroupMaster
    {
        string sp_name = "USP_SubGroupMaster";
        public async Task<spOutputParameter> InsertSubGroup(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblSubGroupMaster model)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "INSERT");
                parameters.Add("p_Code", model.Code);
                parameters.Add("p_GroupName", model.GroupName);
                parameters.Add("p_SubGroupName", model.SubGroupName);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);
                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }

        public async Task<spOutputParameter> DeleteSubGroup(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_GroupName", null);
                parameters.Add("p_SubGroupName", null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }

        public async Task<IEnumerable<dynamic>> ShowSubGroup(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_GroupName", null);
                parameters.Add("p_SubGroupName", null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

        public async Task<IEnumerable<dynamic>> ShowSubGroupByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW_BY_CODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_GroupName", null);
                parameters.Add("p_SubGroupName", null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
    }
}
