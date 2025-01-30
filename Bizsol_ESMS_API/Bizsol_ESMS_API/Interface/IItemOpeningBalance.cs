using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IItemOpeningBalance
    {
        public abstract Task<IEnumerable<dynamic>> GetItemOpeningBalance(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> DeleteOpeningBalance(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<dynamic> SaveItemOpeningBalance(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblItemOpeningBalance model, int UserMaster_Code);
       
    }
}
