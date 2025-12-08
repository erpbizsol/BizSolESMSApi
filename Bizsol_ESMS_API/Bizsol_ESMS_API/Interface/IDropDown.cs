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
        public abstract Task<IEnumerable<dynamic>> GetAccountIsVendorDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetAccountIsClientDropDown(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetOrderNoList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetUserNameList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetDesignationList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetReportType(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetImportFormat(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetBrandType(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string BrandName);
        public abstract Task<IEnumerable<dynamic>> GetBrandList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetClientWiseOrderNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int ClientMaster_Code);
        public abstract Task<IEnumerable<dynamic>> VendorWiseBrandList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int VendorMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetVendorWiseOrderNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int VendorMaster_Code);

    }
}
