using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
namespace Bizsol_ESMS_API.Service
{
    public class ReportService : IReport
    {
        public async Task<IEnumerable<dynamic>> GetLocationReport(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string Templete)
        {
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var data = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                    _bizsolESMSConnectionDetails.DefultMysqlTemp,
                    $"SELECT ProcedureName FROM Location_information WHERE ReportName=@ReportName",
                    new Dictionary<string, object> { { "@ReportName", Templete } })[0];

                if (data.Rows.Count > 0)
                {
                    string procedureName = data.Rows[0]["ProcedureName"].ToString();
                    var result = CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(
                        _bizsolESMSConnectionDetails.DefultMysqlTemp,
                        $"CALL {procedureName}(@p_ReportName)",
                        new Dictionary<string, object> { { "@p_ReportName", Templete } })[0];
                    return CommonFunctions.DatatableToDynamicList(result);
                }
                return new List<dynamic>();
            }
        }

     
    }
}
