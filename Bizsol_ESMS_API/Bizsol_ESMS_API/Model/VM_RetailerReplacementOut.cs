namespace Bizsol_ESMS_API.Model
{
    public class VM_RetailerReplacementOut
    {
        public IEnumerable<tblRetailerReplacementOut> RetailerReplacementOut { get; set; } = [];
        public IEnumerable<tblRetailerReplacementOutDetail> RetailerReplacementOutDetail { get; set; } = [];
    }
}
