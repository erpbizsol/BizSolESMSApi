using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IDropDown
    {
        public abstract Task<IEnumerable<dynamic>> GetDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
    }
}
