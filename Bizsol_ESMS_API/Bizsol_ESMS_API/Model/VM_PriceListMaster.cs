namespace Bizsol_ESMS_API.Model
{
    public class VM_PriceListMaster
    {
        public IEnumerable<tblPriceListMaster> PriceListMaster { get; set; }
        public IEnumerable<tblPriceListDetail> PriceListDetail { get; set; }
    }
}
