namespace Bizsol_ESMS_API.Model
{
    public class VM_BillMasterList
    {
        public IEnumerable<dynamic> BillMaster { get; set; } = [];
        public IEnumerable<dynamic> BillAdjustmentDetails { get; set; } = [];
    }
}
