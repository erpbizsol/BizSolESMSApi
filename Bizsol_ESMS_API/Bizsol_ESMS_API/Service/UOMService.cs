
using System.Data.SqlClient;
using System.Data;
using Bizsol_ESMS_API.Model;
using MySql.Data.MySqlClient;
using Dapper;
using Bizsol_ESMS_API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
namespace Bizsol_ESMS_API.Service
{
    public class UOMService: IUOM
    {
        string sp_name = "USP_UOMMaster";
        public async Task<spOutputParameter> InsertUOM(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, tblUOMMaster uommodel)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                 

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "INSERT");
                parameters.Add("p_Code", uommodel.Code);
                parameters.Add("p_UOMName", uommodel.UOMName);
                parameters.Add("p_DigitAfterDecimal", uommodel.DigitAfterDecimal);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter; 
            }
        }

        public async Task<spOutputParameter> DeleteUOM(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int code)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
               

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "DELETE");
                parameters.Add("p_Code", code);
                parameters.Add("p_UOMName", null);
                parameters.Add("p_DigitAfterDecimal",null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                await conn.QueryAsync(sp_name, parameters, commandType: CommandType.StoredProcedure);

                spOutputParameter outputParameter = new spOutputParameter();
                outputParameter.Msg = parameters.Get<string>("O_Message");
                outputParameter.Status = parameters.Get<string>("O_Status");
                return outputParameter;
            }
        }

        public async Task<IEnumerable<dynamic>> ShowUOM(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {
        
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "SHOW");
                parameters.Add("p_Code", null);
                parameters.Add("p_UOMName", null);
                parameters.Add("p_DigitAfterDecimal", null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

        public async Task<IEnumerable<dynamic>> ShowUOMMasterByCode(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails,int code)
        {
            using (IDbConnection conn = new
            MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
               
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Operation", "SHOW_BY_CODE");
                parameters.Add("p_Code", code);
                parameters.Add("p_UOMName", null);
                parameters.Add("p_DigitAfterDecimal", null);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

    }
}



