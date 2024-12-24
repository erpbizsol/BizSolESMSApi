using Bizsol_ESMS_API.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class UserGroupMasterService: IUserGroupMaster
    {
        public async Task<IEnumerable<dynamic>> GetUserGroupMasterList(string ConnectionString)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                var result = await conn.QueryAsync<dynamic>("Select Code,GroupName From UserGroupMaster", parameters, commandType: CommandType.Text);

                return result.ToList();
            }
        }

    }
}
