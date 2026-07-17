using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class BankMasterService : IBankMaster
    {
        string sp_name = "USP_BankMaster";

        public async Task<spOutputParameter> InsertBankMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblBankMaster model, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVEDATA");
                parameters.Add("p_Code", model.Code);
                parameters.Add("p_BankName", model.BankName);
                parameters.Add("p_AccountNo", model.AccountNo);
                parameters.Add("p_IFSCCode", model.IFSCCode);
                parameters.Add("p_Branch", model.Branch);
                parameters.Add("p_Type", model.Type);
                parameters.Add("p_DefaultCheck", model.DefaultCheck);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return new spOutputParameter
                {
                    Msg = result?.Msg,
                    Status = result?.Status,
                    Code = result == null ? 0 : Convert.ToInt32(result.Code)
                };
            }
        }

        public async Task<spOutputParameter> DeleteBankMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_BankName", "");
                parameters.Add("p_AccountNo", "");
                parameters.Add("p_IFSCCode", "");
                parameters.Add("p_Branch", "");
                parameters.Add("p_Type", "");
                parameters.Add("p_DefaultCheck", "N");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return new spOutputParameter
                {
                    Msg = result?.Msg,
                    Status = result?.Status,
                    Code = result == null ? 0 : Convert.ToInt32(result.Code)
                };
            }
        }

        public async Task<IEnumerable<dynamic>> ShowBankMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_BankName", "");
                parameters.Add("p_AccountNo", "");
                parameters.Add("p_IFSCCode", "");
                parameters.Add("p_Branch", "");
                parameters.Add("p_Type", "");
                parameters.Add("p_DefaultCheck", "N");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<dynamic>> ShowBankMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GETBYCODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_BankName", "");
                parameters.Add("p_AccountNo", "");
                parameters.Add("p_IFSCCode", "");
                parameters.Add("p_Branch", "");
                parameters.Add("p_Type", "");
                parameters.Add("p_DefaultCheck", "N");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
