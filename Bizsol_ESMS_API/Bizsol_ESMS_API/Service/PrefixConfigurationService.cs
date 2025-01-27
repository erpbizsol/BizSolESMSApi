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
        public async Task<IEnumerable<dynamic>> GetPerPageSizeList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_PerPageOption", "");
                parameters.Add("p_PerPage", 0);

                var result = await conn.QueryAsync<dynamic>("USP_PerPageSizeConfiguration", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> SavePerPageSizeConfiguration(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblPerPageSize PerPageSize)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "Save");
                parameters.Add("p_Code", PerPageSize.Code);
                parameters.Add("p_PerPageOption", PerPageSize.PerPageOption);
                parameters.Add("p_PerPage", PerPageSize.PerPage);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_PerPageSizeConfiguration", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
