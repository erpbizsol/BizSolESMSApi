using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class ReasonMasterService : IReasonMaster
    {
        public async Task<IEnumerable<dynamic>> GetReasonMasterList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_ForUse", "SaleReturn");
                var result = await conn.QueryAsync<dynamic>("USP_GetReasonMasterByForUse", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<dynamic>> GetReasonMasterByForUse(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,string ForUse)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_ForUse", ForUse);
                var result = await conn.QueryAsync<dynamic>("USP_GetReasonMasterByForUse", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
