using Bizsol_ESMS_API.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class DesignationMasterService: IDesignationMaster
    {
        public async Task<IEnumerable<dynamic>> GetDesignationMasterList(string ConnectionString)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                var result = await conn.QueryAsync<dynamic>("Select Code,DesignationName From DesignationMaster", parameters, commandType: CommandType.Text);

                return result.ToList();
            }
        }
    }
}
