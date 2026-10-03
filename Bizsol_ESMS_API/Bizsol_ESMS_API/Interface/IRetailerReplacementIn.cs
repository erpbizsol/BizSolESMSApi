using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IRetailerReplacementIn
    {
        public abstract Task<IEnumerable<dynamic>> ShowRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<VM_RetailerReplacementInList> ShowRetailerReplacementInByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_RetailerReplacementIn model);
        public abstract Task<dynamic> DeleteRetailerReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<IEnumerable<dynamic>> GetBrandMasterListForReplacement(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
    }
}
