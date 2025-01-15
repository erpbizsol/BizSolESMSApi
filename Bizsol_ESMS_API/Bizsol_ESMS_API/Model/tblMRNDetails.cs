namespace Bizsol_ESMS_API.Model
{
    public class tblMRNDetails
    {
        public int Code { get; set; }
        public string? ItemName { get; set; } = "";
        public double BillQtyBox { get; set; }
        public double ReceivedQtyBox { get; set; }
        public double BillQty { get; set; }
        public double ReceivedQty { get; set; }
        public double ItemRate { get; set; }
        public double Amount { get; set; }
        public string? WarehouseName { get; set; } = "";
        public string? Remarks { get; set; } = "";
    }
}
