using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class HelpdeskService: IHelpDesk
    {

        string sp_name = "USP_Helpdesk";
        public async Task<IEnumerable<dynamic>> ShowHelpdesk(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<dynamic> ShowHelpdeskByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
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
        public async Task<dynamic> DeleteHelpdesk(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
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
        public async Task<dynamic> SaveHelpdesk(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblHelpdesk Helpdesk)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(Helpdesk);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Helpdesk.Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", Helpdesk.UserMaster_Code);
                parameters.Add("p_jsonData", json);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> CompleteTicket(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "COMPLETE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
