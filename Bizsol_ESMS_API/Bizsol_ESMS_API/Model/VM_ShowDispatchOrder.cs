namespace Bizsol_ESMS_API.Model
{
    public class VM_ShowDispatchOrder
    {
        public IEnumerable<dynamic> DispatchMaster { get; set; } = [];
        public IEnumerable<dynamic> DispatchDetails { get; set; } = [];
    }
}
