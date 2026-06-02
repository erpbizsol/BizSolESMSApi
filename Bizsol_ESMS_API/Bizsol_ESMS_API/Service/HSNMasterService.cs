using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;

namespace Bizsol_ESMS_API.Service
{
    public class HSNMasterService : IHSNMaster
    {
        private const string SpName = "USP_HSNMaster";

        public async Task<spOutputParameter> InsertHSN(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblHSNMaster model, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "INSERT");
                parameters.Add("p_Code", model.Code);
                parameters.Add("p_HSNCode", model.HSNCode);
                parameters.Add("p_GSTRate", model.GSTRate);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 1);
                await conn.QueryAsync(SpName, parameters, commandType: CommandType.StoredProcedure);

                return new spOutputParameter
                {
                    Msg = parameters.Get<string>("O_Message"),
                    Status = parameters.Get<string>("O_Status")
                };
            }
        }

        public async Task<spOutputParameter> DeleteHSN(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_HSNCode", null);
                parameters.Add("p_GSTRate", null);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 1);
                await conn.QueryAsync(SpName, parameters, commandType: CommandType.StoredProcedure);

                return new spOutputParameter
                {
                    Msg = parameters.Get<string>("O_Message"),
                    Status = parameters.Get<string>("O_Status")
                };
            }
        }

        public async Task<IEnumerable<dynamic>> ShowHSN(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_HSNCode", null);
                parameters.Add("p_GSTRate", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 1);
                var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<dynamic>> ShowHSNMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW_BY_CODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_HSNCode", null);
                parameters.Add("p_GSTRate", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 1);
                var result = await conn.QueryAsync<dynamic>(SpName, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
