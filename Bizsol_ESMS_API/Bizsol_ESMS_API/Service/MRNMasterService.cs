using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy.Json;
using Org.BouncyCastle.Asn1;
using System.Data;

namespace Bizsol_ESMS_API.Service
{
    public class MRNMasterService:IMRNMaster
    {

        string sp_name = "USP_MRNMaster";
        public async Task<IEnumerable<dynamic>> GetMRNMasterList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "LOCATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", "");
                parameters.Add("p_ItemName", "");

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
        public async Task<VM_MRNMasterList> GetMRNMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code)
        {
            VM_MRNMasterList vM_MRNMaster = new VM_MRNMasterList();
            var parameters = new Dictionary<string, object>
            {
                { "@p_Mode", "SHOWDATA" },
                { "@p_UserMaster_Code", 0 },
                { "@p_Code",Code},
                { "@p_jsonData", "{}" },
                { "@p_jsonData1", "{}" },
                { "@p_AccountName", "" },
                { "@p_ItemName", "" },

            };

            var dataTables = await Task.Run(() => CommonFunctions.DataTableArrayExecuteSqlQueryWithParameter(bizsolESMSConnectionDetails.DefultMysqlTemp,
                    "call USP_MRNMaster(@p_Mode,@p_UserMaster_Code,@p_Code,@p_jsonData, @p_jsonData1,@p_AccountName,@p_ItemName)",
                    parameters,
                    CommandType.Text
                ));
            vM_MRNMaster.MRNMaster = CommonFunctions.DatatableToDynamicList(dataTables[0]);
            vM_MRNMaster.MRNDetails = CommonFunctions.DatatableToDynamicList(dataTables[1]);
            return vM_MRNMaster;
        }
        public async Task<dynamic> SaveMRNMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_MRNMaster vmMRNMaster,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(vmMRNMaster.MRNMaster);
                var json1 = new JavaScriptSerializer().Serialize(vmMRNMaster.MRNDetails);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", vmMRNMaster.MRNMaster.FirstOrDefault().Code);
                parameters.Add("p_Mode", "SAVE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", json);
                parameters.Add("p_jsonData1", json1);
                parameters.Add("p_AccountName", 0);
                parameters.Add("p_ItemName", "");

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> DeleteMRNMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int Code,int UserMaster_Code)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", Code);
                parameters.Add("p_Mode", "DELETE");
                parameters.Add("p_UserMaster_Code", UserMaster_Code);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", 0);
                parameters.Add("p_ItemName", "");

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> GetRateByVendor(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string VendorName,string ItemName)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("p_Mode", "GETRATE");
                parameters.Add("p_Code", 0);
                parameters.Add("p_UserMaster_Code", 0);
                parameters.Add("p_jsonData", "{}");
                parameters.Add("p_jsonData1", "{}");
                parameters.Add("p_AccountName", VendorName.Trim());
                parameters.Add("p_ItemName", ItemName.Trim());

                var result = await conn.QueryAsync<dynamic>(sp_name, parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        public async Task<dynamic> ImportMRNMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportMRNMaster ImportMRNMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(ImportMRNMaster.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code",0);
                parameters.Add("p_Mode","SAVE");
                parameters.Add("p_AccountName", ImportMRNMaster.VendorName);
                parameters.Add("p_VehicleNo", ImportMRNMaster.VehicleNo);
                parameters.Add("p_UserMaster_Code", ImportMRNMaster.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>("InsertPicklistFromJSON", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<dynamic> ImportMRNMasterForTemp(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblImportMRNMaster ImportMRNMaster)
        {
            using (IDbConnection conn = new MySqlConnection(bizsolESMSConnectionDetails.DefultMysqlTemp))
            {
                var json = new JavaScriptSerializer().Serialize(ImportMRNMaster.JsonData);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_Code", 0);
                parameters.Add("p_Mode","GET");
                parameters.Add("p_AccountName", ImportMRNMaster.VendorName);
                parameters.Add("p_VehicleNo", ImportMRNMaster.VehicleNo);
                parameters.Add("p_UserMaster_Code", ImportMRNMaster.UserMaster_Code);
                parameters.Add("p_jsonData", json);

                var result = await conn.QueryAsync<dynamic>("InsertPicklistFromJSON", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
