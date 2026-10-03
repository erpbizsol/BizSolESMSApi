namespace Bizsol_ESMS_API.Model
{
    public class VM_VendorReplacementOut
    {
        public IEnumerable<tblVendorReplacementOut> VendorReplacementOut { get; set; } = [];
        public IEnumerable<tblVendorReplacementOutDetail> VendorReplacementOutDetail { get; set; } = [];
    }
}
