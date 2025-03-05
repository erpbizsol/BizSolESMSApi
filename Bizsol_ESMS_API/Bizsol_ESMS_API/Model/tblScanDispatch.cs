namespace Bizsol_ESMS_API.Model
{
    public class tblScanDispatch
    {
        public int Code { get; set; }
        public string ScanNo { get; set; }
        public int UserMaster_Code { get; set; }
        public int ScanQty { get; set; }
        public int ManualQty { get; set; }
        public int DispatchQty { get; set; }
        public int DispatchMaster_Code { get; set; }
        public string? PackedBy { get; set; }
    }
}
