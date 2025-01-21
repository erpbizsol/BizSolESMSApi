using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class ItemOpeningBalanceService: IItemOpeningBalance
    {
        public async Task<IEnumerable<dynamic>> GetItemOpeningBalance(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");

                var result = await conn.QueryAsync<dynamic>("USP_ItemOpeningBalance", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> SaveItemOpeningBalance(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,tblItemOpeningBalance ItemOpeningBalance, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(ItemOpeningBalance);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_ItemOpeningBalance", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
