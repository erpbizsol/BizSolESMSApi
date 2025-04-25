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

                string Query = "Select code ,Desp From ReasonMaster";
                var result = await conn.QueryAsync<dynamic>(Query, parameters, commandType: CommandType.Text);
                return result.ToList();

            }
        }

    }
}
