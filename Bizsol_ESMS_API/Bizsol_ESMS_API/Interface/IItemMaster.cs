using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IItemMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowItemByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblItemMaster model,int UserMaster_Code);
        public abstract Task<dynamic> DeleteItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);

        public abstract Task<IEnumerable<dynamic>> GetItemDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
    }                                                                               
}
