using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IBankMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowBankMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowBankMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertBankMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblBankMaster model, int UserMaster_Code);
        public abstract Task<spOutputParameter> DeleteBankMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
