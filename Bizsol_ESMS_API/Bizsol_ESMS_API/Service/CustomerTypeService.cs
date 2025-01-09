using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class CustomerTypeService: ICustomerType
    {
        
        string sp_name = "USP_AccountMaster";
        public async Task<IEnumerable<dynamic>> ShowAccountMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<VM_AccountMasterForShow> ShowAccountMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            VM_AccountMasterForShow vM_AccountMaster =new VM_AccountMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "ShowData" },
                { "@p_Code", Code },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" }
            };

            var dataTables = await Task.Run(() =>CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_AccountMaster(@p_Mode,@p_Code,@p_jsonData, @p_jsonData1)",
                    parameters,
                    CommandType.Text
                ));
            vM_AccountMaster.AccountMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_AccountMaster.AccountAddress =CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_AccountMaster;
        }
        public async Task<dynamic> DeleteAccountMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> InsertAccountMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_AccountMaster vmAccountMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmAccountMaster.AccountMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmAccountMaster.AccountAddress);
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code", vmAccountMaster.AccountMaster.FirstOrDefault().Code); 
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}

