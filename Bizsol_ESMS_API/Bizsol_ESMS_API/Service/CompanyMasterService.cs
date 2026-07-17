using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class CompanyMasterService : ICompanyMaster
    {
        string sp_name = "USP_CompanyMaster";
        public async Task<IEnumerable<dynamic>> GetCompanyMasterList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_jsonData", "{}");
                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<dynamic> SaveCompanyMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblCompanyMaster companyMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var jsonData = new
                {
                    companyCode = companyMaster.CompanyCode,
                    companyName = companyMaster.CompanyName,
                    aliasName = companyMaster.AliasName,
                    addressLine1 = companyMaster.AddressLine1,
                    addressLine2 = companyMaster.AddressLine2,
                    cityName = companyMaster.CityName,
                    nation = companyMaster.Nation,
                    pin = companyMaster.PIN,
                    pANNo = companyMaster.PANNo,
                    gstNo = companyMaster.GSTNo,
                    phone = companyMaster.Phone,
                    mobileNo = companyMaster.MobileNo,
                    email = companyMaster.Email,
                    mSMENo = companyMaster.MSMENo,
                    uPIId = companyMaster.UPIId
                };
                var json = new JavaScriptSerializer().Serialize(jsonData);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_Code", companyMaster.Code);
                parameters.Add("p_jsonData", json);
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
