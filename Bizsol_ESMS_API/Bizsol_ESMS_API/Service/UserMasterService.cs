using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using Nancy.Json;

namespace Bizsol_ESMS_API.Service
{
    public class UserMasterService:IUserMaster
    {
        string sp_name = "USP_UserMaster";
        public async Task<IEnumerable<dynamic>> GetUserMasterList(string ConnectionString,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> GetUserMasterListByCode(string ConnectionString, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SHOWDATA");
                parameters.Add("p_Code", Code);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteUserMaster(string ConnectionString, int Code, int UserMaster_Code,string ReasonForDelete)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> SaveUserMaster(string ConnectionString, tblUserMaster tblUserMaster)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                var Data = CommonFunctions.EncryptPasswordAsync(tblUserMaster.Password);
                tblUserMaster.Password = Data.Result;
                var json = new JavaScriptSerializer().Serialize(tblUserMaster);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", tblUserMaster.Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", tblUserMaster.UserMaster_Code);
                parameters.Add("p_jsonData", json);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
