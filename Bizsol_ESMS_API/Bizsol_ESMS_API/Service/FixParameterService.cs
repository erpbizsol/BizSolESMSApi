using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class FixParameterService : IFixParameter
    {
        string sp_name = "USP_FixParameterConfiguration";

        public async Task<dynamic> VerifyFixParameterConfigAccess(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int userMasterCode, string? password)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                string mode = string.IsNullOrWhiteSpace(password) ? "CHECKUSER" : "VALIDATEPWD";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", mode);
                parameters.Add("p_Id", 0);
                parameters.Add("p_JsonData", null);
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_Password", password ?? string.Empty);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> SaveFixParameter(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int userMasterCode, string password, string jsonData)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "UPDATE");
                parameters.Add("p_Id", 0);
                parameters.Add("p_JsonData", jsonData);
                parameters.Add("p_UserMaster_Code", userMasterCode);
                parameters.Add("p_Password", password ?? string.Empty);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
