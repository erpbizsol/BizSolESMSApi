namespace Bizsol_ESMS_API.Model
{
    public class VM_BillMaster
    {
        public IEnumerable<tblBillMaster> BillMaster { get; set; }
        public IEnumerable<tblBillAdjustmentDetails> BillAdjustmentDetails { get; set; }
    }
}
