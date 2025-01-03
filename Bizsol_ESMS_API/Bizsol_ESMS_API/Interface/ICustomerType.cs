using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ICustomerType
    {
        public abstract Task<IEnumerable<dynamic>> ShowAccountMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowAccountMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertAccountMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblCustomerType model);
        public abstract Task<dynamic> DeleteAccountMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
