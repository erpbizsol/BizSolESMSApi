namespace Bizsol_ESMS_API.Interface
{
    public interface IDesignationMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetDesignationMasterList(string ConnectionString);
    }
}
