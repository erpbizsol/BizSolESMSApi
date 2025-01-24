using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class PrefixConfigurationService: IPrefixConfiguration
    {
        public async Task<IEnumerable<dynamic>> GetPrefixConfigurationList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_MRNNo", "");
                parameters.Add("p_OrderNo", "");
                parameters.Add("p_ChallanNo", "");

                var result = await conn.QueryAsync<dynamic>("USP_PrefixConfiguration", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> SavePrefixConfiguration(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblPrefixConfiguration PrefixConfiguration)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_MRNNo", PrefixConfiguration.MRNNo);
                parameters.Add("p_OrderNo", PrefixConfiguration.OrderNo);
                parameters.Add("p_ChallanNo", PrefixConfiguration.ChallanNo);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_PrefixConfiguration", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
