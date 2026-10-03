using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IVendorReplacementIn
    {
        public abstract Task<IEnumerable<dynamic>> ShowVendorReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<VM_VendorReplacementInList> ShowVendorReplacementInByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<VM_VendorReplacementInList> ShowDataByEntryNo(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveVendorReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_VendorReplacementIn model);
        public abstract Task<dynamic> DeleteVendorReplacementIn(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<IEnumerable<dynamic>> GetEntryNoList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
    }
}
