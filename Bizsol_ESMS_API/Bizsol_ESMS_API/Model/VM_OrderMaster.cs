namespace Bizsol_ESMS_API.Model
{
    public class VM_OrderMaster
    {
        public IEnumerable<tblOrder> OrderMaster { get; set; }
        public IEnumerable<tblOrderDetials> OrderDetial { get; set; } 
    }
}
