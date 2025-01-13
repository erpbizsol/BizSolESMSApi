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
        public abstract Task<IEnumerable<dynamic>> GetCountryDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails); 
        public abstract Task<IEnumerable<dynamic>> GetStateDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails); 
        public abstract Task<IEnumerable<dynamic>> GetCityDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetWhereHouseDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetAccountDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);

    }
}
