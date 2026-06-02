using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class LocationMasterService: ILocationMaster
    {
        string sp_name = "USP_LocationMaster";
        public async Task<spOutputParameter> InsertLocationMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblLocationMaster Model, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "INSERT");
                parameters.Add("p_Code", Model.Code);
                parameters.Add("p_LocationName", Model.LocationName);
                parameters.Add("p_Location", Model.Location);
                parameters.Add("p_LocationGroup", Model.LocationGroup);
                parameters.Add("p_Mode", Model.Mode);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }
        public async Task<spOutputParameter> DeleteLocationMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_LocationName", null);
                parameters.Add("p_Location", null);
                parameters.Add("p_LocationGroup", null);
                parameters.Add("p_Mode", null);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }
        public async Task<IEnumerable<dynamic>> ShowLocationMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_LocationName", null);
                parameters.Add("p_Location", null);
                parameters.Add("p_LocationGroup", null);
                parameters.Add("p_Mode", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> ShowLocationMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW_BY_CODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_LocationName", null);
                parameters.Add("p_Location", null);
                parameters.Add("p_LocationGroup", null);
                parameters.Add("p_Mode", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<dynamic> CreateLocationFromItemMaster(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblLocationMaster Model, int UserMaster_Code, string IsCheckExists)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", (Model.Mode ?? "").Trim());
                parameters.Add("p_ItemMaster_Code", Model.Code);
                parameters.Add("p_LocationName", Model.LocationName);
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_IsCheckExists", IsCheckExists.Trim());
                var result = await conn.QueryAsync("USP_CreateLocationFromItemMaster", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        public async Task<dynamic> GetItemLocationMaster_Code(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,int Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GETLOCATION");
                parameters.Add("p_ItemMaster_Code",Code);
                parameters.Add("p_LocationName","" );
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_IsCheckExists", "");
                var result = await conn.QueryAsync("USP_CreateLocationFromItemMaster", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        public async Task<dynamic> ImportLocation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportLocation importLocation)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(importLocation.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_WarehouseMaster_Code", importLocation.WarehouseMaster_Code);
                parameters.Add("p_InsertNewItem", importLocation.InsertNewItem);
                parameters.Add("p_InsertNewLocation", importLocation.InsertNewLocation);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("USP_ImportLocation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<dynamic> ImportLocationForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportLocation importLocation)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(importLocation.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "GET");
                parameters.Add("p_UserMaster_Code", bizsolESMSConnectionDetails.UserMaster_Code);
                parameters.Add("p_WarehouseMaster_Code", importLocation.WarehouseMaster_Code);
                parameters.Add("p_InsertNewItem","");
                parameters.Add("p_InsertNewLocation","");
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("USP_ImportLocation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
