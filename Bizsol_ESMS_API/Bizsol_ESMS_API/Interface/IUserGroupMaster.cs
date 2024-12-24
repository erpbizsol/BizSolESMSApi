namespace Bizsol_ESMS_API.Interface
{
    public interface IUserGroupMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetUserGroupMasterList(string ConnectionString);
    }
}
