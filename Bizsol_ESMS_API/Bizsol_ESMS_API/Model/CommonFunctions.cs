
using Bizsol_ESMS_API.Model;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Bizsol_ESMS_API.Model
{
    public static class CommonFunctions
    {
        public static BizsolESMSConnectionDetails InitializeERPConnection(HttpContext httpContext)
        {

            StringValues AutJsonKey = "";
            BizsolESMSConnectionDetails bizsolESMSConnectionDetails = new BizsolESMSConnectionDetails();

            if (httpContext.Request.Headers.TryGetValue("Auth-Key", out AutJsonKey))
            {
                bizsolESMSConnectionDetails = JsonConvert.DeserializeObject<BizsolESMSConnectionDetails>(AutJsonKey);
            }

            return bizsolESMSConnectionDetails;
        }
        public static async Task<string> EncryptPasswordAsync(string password)
        {
            StringBuilder encryptedPassword = new StringBuilder();
            foreach (char c in password)
            {
                encryptedPassword.Append(Convert.ToChar(Convert.ToInt32(c) + 10));
            }
            await Task.CompletedTask;
            return encryptedPassword.ToString();
        }

        public static async Task<string> DecryptPasswordAsync(string encryptedPassword)
        {
            StringBuilder decryptedPassword = new StringBuilder();
            foreach (char c in encryptedPassword)
            {
                decryptedPassword.Append(Convert.ToChar(Convert.ToInt32(c) - 10));
            }
            await Task.CompletedTask;
            return decryptedPassword.ToString();
        }

        public static List<DataTable> DataTableArrayExecuteSqlQueryWithParameter(string conStr, string queryString, Dictionary<string, object> Params, CommandType commandType = CommandType.Text)
        {
            List<DataTable> dt = new List<DataTable>();
            using (var connection = new MySqlConnection(conStr))
            {
                var command = new MySqlCommand(queryString, connection);
                command.CommandType = commandType;
                command.CommandTimeout = 0;
                connection.Open();
                if (Params != null)
                {
                    foreach (KeyValuePair<string, object> p in Params)
                    {
                        var dbParameter = command.CreateParameter();
                        dbParameter.ParameterName = p.Key;
                        dbParameter.Value = p.Value;
                        command.Parameters.Add(dbParameter);
                    }
                }
                using (var SqlDA = new MySqlDataAdapter())
                {
                    DataSet ds = new DataSet();
                    SqlDA.SelectCommand = command;
                    SqlDA.Fill(ds);

                    foreach (var idt in ds.Tables)
                    {
                        dt.Add((DataTable)idt);
                    }
                    return dt;
                }
            }

        }
        public static List<Dictionary<string, object>> DatatableToDynamicList(DataTable dataTable)
        {
            return dataTable.AsEnumerable()
                            .Select(r => r.Table.Columns.Cast<DataColumn>()
                                         .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal]))
                                         .ToDictionary(z => z.Key, z => z.Value))
                                         .ToList();
        }
    }
}
