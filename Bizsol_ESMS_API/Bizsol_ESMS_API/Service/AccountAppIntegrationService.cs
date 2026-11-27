using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySqlConnector;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class AccountAppIntegrationService : IAccountAppIntegration
    {
        string sp_name = "USP_AccountAppIntegration";
        public async Task<IEnumerable<dynamic>> GetTallybusyTempletConfigrution(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "TEMPLATE");
                parameters.Add("p_Template", null);
                parameters.Add("p_FromDate", null);
                parameters.Add("p_ToDate", null);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }
        public async Task<IEnumerable<dynamic>> GetAccountAppIntegrationTemplateData(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string template, string fromDate, string toDate)
        {

            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Operation", "SHOW");
                parameters.Add("p_Template", template);
                parameters.Add("p_FromDate", fromDate);
                parameters.Add("p_ToDate", toDate);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
        }

        public async Task<IEnumerable<dynamic>> SyncToSqlServer(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string template, string templateName, string fromDate, string toDate, List<string> codes, int userCode = 0)
        {
            static object[] Fail(string msg) => new object[] { new { Status = "N", Msg = msg, Inserted = 0, Updated = 0, Skipped = 0 } };

            dynamic? cfg;
            using (IDbConnection conn = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Operation", "SQLCONFIG");
                p.Add("p_Template", null);
                p.Add("p_FromDate", null);
                p.Add("p_ToDate", null);
                p.Add("p_UserMaster_Code", _bizsolESMSConnectionDetails.UserMaster_Code);
                p.Add("O_Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                p.Add("O_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                cfg = (await conn.QueryAsync<dynamic>(sp_name, p, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
            if (cfg == null) return Fail("Active SQL Server configuration not found (F_TallyDBConfigrution).");

            var c = (IDictionary<string, object>)cfg;
            string Val(string k) => c.TryGetValue(k, out var v) && v != null ? Convert.ToString(v)!.Trim() : "";

            string server = Val("sqladdress");
            if (server == "") server = Val("VpnSqlIpAddress");
            string port = Val("MySqlPort");
            if (server == "") return Fail("SQL Server address is empty in F_TallyDBConfigrution.");

            var csb = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder
            {
                DataSource = port != "" ? $"{server},{port}" : server,
                InitialCatalog = Val("LoginDatabase"),
                UserID = Val("userid"),
                Password = Val("pwd"),
                TrustServerCertificate = true,
                Encrypt = false,
                ConnectTimeout = 15
            };

            // 2. MySQL view rows (same proc jo grid ke liye chalta hai)
            var rows = (await GetAccountAppIntegrationTemplateData(_bizsolESMSConnectionDetails, template, fromDate, toDate)).ToList();
            if (rows.Count == 0) return Fail("Record not found.");
            var first = (IDictionary<string, object>)rows[0];
            if (first.ContainsKey("Status") && Convert.ToString(first["Status"]) == "N") return Fail("Invalid template.");

            // 3. selected Code ke hisaab se group (ek Code ki kai rows ho sakti hain)
            const string keyColumn = "Code";
            var wanted = new HashSet<string>((codes ?? new List<string>()).Select(x => x?.Trim() ?? ""), StringComparer.OrdinalIgnoreCase);
            if (wanted.Count == 0) return Fail("Please select at least one record.");

            string? updCol = first.Keys.FirstOrDefault(k =>
            {
                var n = k.Replace(" ", "").Replace("_", "").ToLowerInvariant();
                return n is "updatedate" or "updateddate" or "modifiedon" or "modifieddate" or "lastupdatedate" or "editdate";
            });

            var docs = new List<Dictionary<string, object?>>();
            foreach (var g in rows
                .Select(r => (IDictionary<string, object>)r)
                .Where(r => r.ContainsKey(keyColumn) && r[keyColumn] != null && wanted.Contains(Convert.ToString(r[keyColumn])!.Trim()))
                .GroupBy(r => Convert.ToString(r[keyColumn])!.Trim()))
            {
                DateTime? maxUpd = null;
                if (updCol != null)
                {
                    foreach (var r in g)
                        if (r[updCol] is DateTime d && (maxUpd == null || d > maxUpd)) maxUpd = d;
                }
                docs.Add(new Dictionary<string, object?>
                {
                    ["Code"] = g.Key,
                    ["UpdateDate"] = maxUpd?.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                    ["Data"] = g.Select(r => r.ToDictionary(kv => kv.Key, kv => kv.Value)).ToList()
                });
            }
            if (docs.Count == 0) return Fail("Selected records not found.");

            // & ' < > + ko \uXXXX me escape na kare (column naam jaise "Name & Address" same rahe)
            var jsonOpts = new System.Text.Json.JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = System.Text.Json.JsonSerializer.Serialize(docs, jsonOpts);

            // 4. SQL Server proc : insert (nahi hai) / update (UpdateDate bada)
            try
            {
                using (var sql = new Microsoft.Data.SqlClient.SqlConnection(csb.ConnectionString))
                {
                    var sp = new DynamicParameters();
                    sp.Add("@Template", string.IsNullOrWhiteSpace(templateName) ? template : templateName.Trim());
                    sp.Add("@JsonData", json, DbType.String);
                    var result = (await sql.QueryAsync<dynamic>("USP_TallyBusySync", sp, commandType: CommandType.StoredProcedure, commandTimeout: 120)).ToList();

                    // 5. SQL Server sync SUCCESS -> MySQL *details tables me entry (USP_TallySyncLog)
                    var st = result.FirstOrDefault() as IDictionary<string, object>;
                    if (st != null && st.TryGetValue("Status", out var stv) && Convert.ToString(stv) == "Y")
                    {
                        try
                        {
                            using IDbConnection my = new MySqlConnection(_bizsolESMSConnectionDetails.DefultMysqlTemp);
                            await my.ExecuteAsync("USP_TallySyncLog", new
                            {
                                p_Template = string.IsNullOrWhiteSpace(templateName) ? template : templateName.Trim(),
                                p_Codes = string.Join(",", docs.Select(d => d["Code"])),
                                p_UserCode = userCode
                            }, commandType: CommandType.StoredProcedure);
                        }
                        catch (Exception ex)
                        {
                            return Fail("SQL Server me data chala gaya, par *details table entry fail: " + ex.Message);
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return Fail("SQL Server error: " + ex.Message);
            }
        }
    }
}
