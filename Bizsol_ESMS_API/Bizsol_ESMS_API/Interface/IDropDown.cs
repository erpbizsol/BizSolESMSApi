using Bizsol_ESMS_API.Model;
using System.Text.RegularExpressions;

namespace Bizsol_ESMS_API.Interface
{
    public interface IDropDown
    {
        public abstract Task<IEnumerable<dynamic>> GetDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetUOMDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetCategoryDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetSubGroupDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetBrandDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetLocationDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
       
    }
}
