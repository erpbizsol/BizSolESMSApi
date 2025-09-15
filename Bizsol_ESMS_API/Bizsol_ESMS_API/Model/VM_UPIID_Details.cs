namespace Bizsol_ESMS_API.Model
{
    public class VM_UPIID_Details
    {
        public IEnumerable<dynamic> DispatchMaster { get; set; } = [];
        public IEnumerable<dynamic>? MRNMaster { get; set; } = [];
        public IEnumerable<dynamic>? SalesReturn { get; set; } = [];
    }
}
