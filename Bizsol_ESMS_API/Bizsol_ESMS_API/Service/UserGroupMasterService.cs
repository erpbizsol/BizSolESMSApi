using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class UserGroupMasterService: IUserGroupMaster
    {
        string sp_name = "USP_UserGroupMaster";
        public async Task<IEnumerable<dynamic>> GetUserGroupMasterList(string ConnectionString)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_UserMaster_Code",0);
                parameters.Add("p_GroupName", "");
                parameters.Add("p_GroupType", "");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> GetUserGroupMasterByCode(string ConnectionString, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "SHOWDATA");
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_GroupName", "");
                parameters.Add("p_GroupType", "");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteUserGroupMaster(string ConnectionString,int Code,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_GroupName", ""); 
                parameters.Add("p_GroupType", "");

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> SaveUserGroupMaster(string ConnectionString, tblUserGroupMaster tblUserMaster)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                var json = new JavaScriptSerializer().Serialize(tblUserMaster);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", tblUserMaster.Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", tblUserMaster.UserMaster_Code);
                parameters.Add("p_GroupName", tblUserMaster.GroupName);
                parameters.Add("p_GroupType", tblUserMaster.GroupType);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
