﻿using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class WarehouseService:IWarehouse
    {
        string sp_name = "USP_WarehouseMaster";
        public async Task<spOutputParameter> InsertWarehouse(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblWarehouse model, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "INSERT");
                parameters.Add("p_Code", model.Code);
                parameters.Add("p_WarehouseName", model.WarehouseName);
                parameters.Add("p_WarehouseType", model.WarehouseType);
                parameters.Add("p_Address", model.Address);
                parameters.Add("p_Pin", model.Pin);
                parameters.Add("p_City", model.City);
                parameters.Add("p_GSTIN", model.GSTIN);
                parameters.Add("p_DefaultWarehouse", model.DefaultWarehouse);
                parameters.Add("p_UserMaster_Code",  UserMaster_Code);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }

        public async Task<spOutputParameter> DeleteWarehouse(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code, int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_WarehouseName", null);
                parameters.Add("p_WarehouseType", null);
                parameters.Add("p_Address", null);
                parameters.Add("p_Pin", null);
                parameters.Add("p_City", null);
                parameters.Add("p_GSTIN", null);
                parameters.Add("p_DefaultWarehouse", null);
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

        public async Task<IEnumerable<dynamic>> ShowWarehouse(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_WarehouseName", null);
                parameters.Add("p_WarehouseType", null);
                parameters.Add("p_Address", null);
                parameters.Add("p_Pin", null);
                parameters.Add("p_City", null);
                parameters.Add("p_GSTIN", null);
                parameters.Add("p_DefaultWarehouse", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

        public async Task<IEnumerable<dynamic>> ShowWarehouseMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW_BY_CODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_WarehouseName", null);
                parameters.Add("p_WarehouseType", null);
                parameters.Add("p_Address", null);
                parameters.Add("p_Pin", null);
                parameters.Add("p_City", null);
                parameters.Add("p_GSTIN", null);
                parameters.Add("p_DefaultWarehouse", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> WarehouseNameForDefault(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "DEFAULT");
                parameters.Add("p_Code", null);
                parameters.Add("p_WarehouseName", null);
                parameters.Add("p_WarehouseType", null);
                parameters.Add("p_Address", null);
                parameters.Add("p_Pin", null);
                parameters.Add("p_City", null);
                parameters.Add("p_GSTIN", null);
                parameters.Add("p_DefaultWarehouse", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

    }
}
