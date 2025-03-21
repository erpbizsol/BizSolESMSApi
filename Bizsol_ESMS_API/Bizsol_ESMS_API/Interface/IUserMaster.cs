﻿using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IUserMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetUserMasterList(string ConnectionString, int UserMaster_Code);
        public abstract Task<dynamic> SaveUserMaster(string ConnectionString, tblUserMaster UserMaster);
        public abstract Task<dynamic> GetUserMasterListByCode(string ConnectionString, int Code);
        public abstract Task<dynamic> DeleteUserMaster(string ConnectionString, int Code, int UserMaster_Code, string Reason);
        public abstract Task<IEnumerable<dynamic>> GetUserModuleMasterList(string ConnectionString);
        public abstract Task<dynamic> SaveUserModuleMaster(string ConnectionString,IEnumerable<tblUserModuleMaster> _tblUserModuleMaster);
        public abstract Task<IEnumerable<dynamic>> GetUserOptionsDetails(string ConnectionString);
        public abstract Task<IEnumerable<dynamic>> GetUserModuleRightsList(string ConnectionString, int CompanyCode, int UserCode);
        public abstract Task<dynamic> CheckUserOptionRight(string ConnectionString,int UserModuleMaster_Code, int UserMaster_Code,string Option);
    }
}
