using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IVendorReplacementOut
    {
        public abstract Task<IEnumerable<dynamic>> ShowVendorReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<VM_VendorReplacementOutList> ShowVendorReplacementOutByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveVendorReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_VendorReplacementOut model);
        public abstract Task<dynamic> DeleteVendorReplacementOut(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<IEnumerable<dynamic>> GetBrandMasterListForReplacement(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
    }
}
