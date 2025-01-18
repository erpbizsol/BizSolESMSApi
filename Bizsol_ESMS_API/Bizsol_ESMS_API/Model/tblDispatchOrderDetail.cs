namespace Bizsol_ESMS_API.Model
{
    public class tblDispatchOrderDetail
    {
        public string? OrderNo { get; set; } = "";
        public string? ItemName { get; set; } = "";
        public double DispatchQty { get; set; }
        public double QtyBox { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string? Remarks { get; set; } = "";
    }
}
