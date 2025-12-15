using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class LoginService : ILogin
    {
        public async Task<dynamic> GetIsActiveDetails(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,int UserMaster_Code,string IPAddress)
        {
            string sp_name = "USP_CheckUserIsActive";
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Check");
                parameters.Add("p_Code", UserMaster_Code);
                parameters.Add("p_IsActive", "");
                parameters.Add("p_IPAddress", IPAddress);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> GetCountIsActiveUser(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            string sp_name = "USP_CheckUserIsActive";
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Count");
                parameters.Add("p_Code", 0);
                parameters.Add("p_IsActive", "");
                parameters.Add("p_IPAddress", "");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> UpdateIsActiveUser(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,int UserMaster_Code,string IsActive,string IPAddress)
        {
            string sp_name = "USP_CheckUserIsActive";
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Update");
                parameters.Add("p_Code", UserMaster_Code);
                parameters.Add("p_IsActive", IsActive);
                parameters.Add("p_IPAddress", IPAddress);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> GetFixParameter(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
               
                var result = await conn.QueryAsync<dynamic>("Select * From FixParameter limit 1;", parameters, commandType: CommandType.Text);
                return result;
            }
        }
    }
}
