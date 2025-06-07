namespace Bizsol_ESMS_API.Model
{
    public class tblManualRateAndQty
    {
        public int DispatchMaster_Code { get; set; }
        public int ManualQty { get; set; }
        public string? ItemCode { get; set; }
        public int OrderMaster_Code { get; set; }
        public decimal MRP { get; set; }
        public int BoxNo { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
