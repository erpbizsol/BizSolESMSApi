using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IConfigItemMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowItemConfig(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<spOutputParameter> SaveConfig(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblConfigItemMaster model);
    }
}
