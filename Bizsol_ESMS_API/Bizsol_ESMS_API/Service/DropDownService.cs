using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class DropDownService:IDropDown
    {
        string sp_name = "USP_GetDropDown";
        public async Task<IEnumerable<dynamic>> GetDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetGroup");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetUOMDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetUOM");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetCategoryDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetCategory");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetSubGroupDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetSubGroupItem");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetBrandDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetBrand");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetLocationDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetItemLocation");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetCountryDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetCountryLocation");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetStateDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetStateData");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetCityDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetCityData");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetWhereHouseDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_UserMaster_Code", _bizsolESMSConnectionDetails.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_GetWarehouseMasterDropDown", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<dynamic>> GetAccountIsVendorDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetAccountIsVendor");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetAccountIsClientDropDown(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetAccountIsClient");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetOrderNoList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetOrderNo");
                parameters.Add("p_UserMaster_Code", _bizsolESMSConnectionDetails.UserMaster_Code);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetUserNameList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "GetUser");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetDesignationList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "Designation");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetReportType(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "ReportType");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetImportFormat(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "Import");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        
        public async Task<IEnumerable<dynamic>> GetBrandType(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string BrandName)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_BrandName", BrandName);
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>("USP_GetBrandTypeByName", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetBrandList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "Brand");
                parameters.Add("p_UserMaster_Code", 0);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>>GetClientWiseOrderNo(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int ClientMaster_Code)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_ClientMaster_Code", ClientMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_ClientWiseOrderNo", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> VendorWiseBrandList(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int VendorMaster_Code)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_VendorMaster_Code", VendorMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_VendorWiseBrand", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetVendorWiseOrderNo(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int VendorMaster_Code)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", VendorMaster_Code);
                var result = await conn.QueryAsync<dynamic>("USP_GetVendorWiseOrderNo", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

    }
}
