using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class CheckRelatedRecordService:ICheckRelatedRecord
    {
        string sp_name = "USP_CheckRelatedRecordsBeforeDeletion";

        public async Task<IEnumerable<dynamic>> ICheckRelatedRecord(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, int p_CodeValue, string p_TableName)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_CodeValue", p_CodeValue);
                parameters.Add("p_TableName", p_TableName);
                parameters.Add("p_HideMessage", "N");
                parameters.Add("p_QueryCondition", "");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
    }
}
