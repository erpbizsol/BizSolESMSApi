using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IRetailerReplacementOut
    {
        public abstract Task<IEnumerable<dynamic>> ShowRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<VM_RetailerReplacementOutList> ShowRetailerReplacementOutByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<VM_RetailerReplacementOutList> ShowDataByEntryNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_RetailerReplacementOut model);
        public abstract Task<dynamic> DeleteRetailerReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<IEnumerable<dynamic>> GetEntryNoList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetBrandMasterListForReplacement(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
    }
}
