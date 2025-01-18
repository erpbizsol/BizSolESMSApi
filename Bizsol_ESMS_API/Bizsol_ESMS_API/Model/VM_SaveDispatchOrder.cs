namespace Bizsol_ESMS_API.Model
{
    public class VM_SaveDispatchOrder
    {
        public IEnumerable<tblDispatchOrder> DispatchMaster { get; set; }
        public IEnumerable<tblDispatchOrderDetail> DispatchDetails { get; set; }
    }
}
