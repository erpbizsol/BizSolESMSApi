using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class HolidayMasterService:IHolidayMaster
    {
        string sp_name = "USP_HolidayMaster";
        public async Task<dynamic> SaveHolidayMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblHolidayMaster HolidayMaster)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVEDATA");
                parameters.Add("p_Code", HolidayMaster.Code);
                parameters.Add("p_Vacation", HolidayMaster.Vacation);
                parameters.Add("p_Date", HolidayMaster.Date);
                parameters.Add("p_UserMaster_Code", HolidayMaster.UserMaster_Code);
                var result= await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteHolidayMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_Code", Code);
                parameters.Add("p_Vacation", "");
                parameters.Add("p_Date","");
                parameters.Add("p_UserMaster_Code",UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<dynamic>> GetHolidayMasterList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_Vacation", "");
                parameters.Add("p_Date", "");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<dynamic>> GetHolidayMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SHOWDATA");
                parameters.Add("p_Code", Code);
                parameters.Add("p_Vacation", "");
                parameters.Add("p_Date", "");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
