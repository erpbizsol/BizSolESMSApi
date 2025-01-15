using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class OrderService:IOrder
    {
        string sp_name = "USP_AccountMaster";
        public async Task<IEnumerable<dynamic>> ShowOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
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
        public async Task<VM_OrderMasterForShow> ShowOrderMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            VM_OrderMasterForShow vM_OrderMaster = new VM_OrderMasterForShow();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "ShowData" },
                { "@p_Code", Code },
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" }
            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_AccountMaster(@p_Mode,@p_Code,@p_jsonData, @p_jsonData1)",
                    parameters,
                    CommandType.Text
                ));
            vM_OrderMaster.OrderMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_OrderMaster.OrderDetial = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_OrderMaster;
        }
        public async Task<dynamic> DeleteOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
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
        public async Task<dynamic> InsertOrderMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_OrderMaster vmOrderMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmOrderMaster.OrderMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmOrderMaster.OrderDetial);
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Code", vmOrderMaster.OrderMaster.FirstOrDefault().Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
