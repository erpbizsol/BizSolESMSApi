namespace Bizsol_ESMS_API.Model
{
    public class tblScanToBill
    {
        public int Code { get; set; }
        public string? ScanNo { get; set; }
        public string? AccountName { get; set; }
        public string? InvoiceNo { get; set; }
        public string? PackedBy { get; set; }
        public int BoxNo { get; set; }
        public int WarehouseMaster_Code { get; set; }
        public int UserMaster_Code { get; set; }
        public string? IsManual { get; set; }
    }
}
