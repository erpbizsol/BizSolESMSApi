using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class StockAuditmasterService: IStockAuditmaster
    {
        public async Task<IEnumerable<dynamic>> GetStockAuditmasterList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_ScanNo", "");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>("USP_StockAuditmaster", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> ScanStockAuditmaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblStockAuditmaster StockAuditmaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode", "SCAN");
                parameters.Add("p_ScanNo", StockAuditmaster.ScanNo);
                parameters.Add("p_UserMaster_Code", StockAuditmaster.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_StockAuditmaster", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> CompleteStockAuditmaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "COMPLETE");
                parameters.Add("p_ScanNo", "");
                parameters.Add("p_Code", Code);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_StockAuditmaster", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
