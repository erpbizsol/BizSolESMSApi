using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IUserGroupMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetUserGroupMasterList(string ConnectionString);
        public abstract Task<dynamic> SaveUserGroupMaster(string ConnectionString,tblUserGroupMaster tblUserGroupMaster);
        public abstract Task<dynamic> GetUserGroupMasterByCode(string ConnectionString,int Code);
        public abstract Task<dynamic> DeleteUserGroupMaster(string ConnectionString, int Code, int UserMaster_Code);
    }
}
