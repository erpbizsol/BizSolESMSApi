using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IItemMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowItemByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblItemMaster model);
        public abstract Task<dynamic> DeleteItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
