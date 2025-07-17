using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class StockAuditConfigService :IStockAuditConfig
    {
        string sp_name = "USP_StockAuditConfig";
        public async Task<dynamic> SaveStockAuditConfig(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblStockAuditConfig StockAuditConfig)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVEDATA");
                parameters.Add("p_Code", StockAuditConfig.Code);
                parameters.Add("p_CategoryMaster_Code", StockAuditConfig.CategoryMaster_Code);
                parameters.Add("p_Value", StockAuditConfig.Value);
                parameters.Add("p_CycleCountDays", StockAuditConfig.CycleCountDays);
                parameters.Add("p_PartLineCount", StockAuditConfig.PartLineCount);
                parameters.Add("p_UserMaster_Code", StockAuditConfig.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteStockAuditConfig(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", Code);
                parameters.Add("p_CategoryMaster_Code", 0);
                parameters.Add("p_Value", "");
                parameters.Add("p_CycleCountDays",0);
                parameters.Add("p_PartLineCount", 0);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetStockAuditConfigList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_CategoryMaster_Code", 0);
                parameters.Add("p_Value", "");
                parameters.Add("p_CycleCountDays", 0);
                parameters.Add("p_PartLineCount", 0);
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<dynamic>> GetStockAuditConfigByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SHOWDATA");
                parameters.Add("p_Code", Code);
                parameters.Add("p_CategoryMaster_Code", 0);
                parameters.Add("p_Value", "");
                parameters.Add("p_CycleCountDays", 0);
                parameters.Add("p_PartLineCount", 0);
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
