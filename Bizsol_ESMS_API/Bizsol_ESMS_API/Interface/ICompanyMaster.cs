using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ICompanyMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetCompanyMasterList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> SaveCompanyMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblCompanyMaster model);
    }
}
