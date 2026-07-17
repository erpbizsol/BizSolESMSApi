namespace Bizsol_ESMS_API.Model
{
    public class tblAddItemScanToBill
    {
        public int DispatchMaster_Code { get; set; }
        public int ItemMaster_Code { get; set; }
        public int BoxNo { get; set; }
        public int ManualQty { get; set; }
        public decimal Mrp { get; set; }
        public string? OrderNo { get; set; }
        public string? PackedBy { get; set; }
        public string? ClientName { get; set; }
        public int WarehouseMaster_Code { get; set; }
        public string? IsManual { get; set; }
    }
}
