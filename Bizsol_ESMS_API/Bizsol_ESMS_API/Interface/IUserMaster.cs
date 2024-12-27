using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IUserMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetUserMasterList(string ConnectionString, int UserMaster_Code);
        public abstract Task<dynamic> SaveUserMaster(string ConnectionString, tblUserMaster UserMaster);
        public abstract Task<dynamic> GetUserMasterListByCode(string ConnectionString, int Code);
        public abstract Task<dynamic> DeleteUserMaster(string ConnectionString, int Code, int UserMaster_Code, string Reason);
        public abstract Task<IEnumerable<dynamic>> GetUserModuleMasterList(string ConnectionString);
    }
}
