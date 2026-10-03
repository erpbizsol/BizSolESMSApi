namespace Bizsol_ESMS_API.Model
{
    public class VM_VendorReplacementIn
    {
        public IEnumerable<tblVendorReplacementIn> VendorReplacementIn { get; set; } = [];
        public IEnumerable<tblVendorReplacementInDetail> VendorReplacementInDetail { get; set; } = [];
    }
}
