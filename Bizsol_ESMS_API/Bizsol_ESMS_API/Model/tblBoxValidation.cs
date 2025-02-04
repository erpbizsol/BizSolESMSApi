namespace Bizsol_ESMS_API.Model
{
    public class tblBoxValidation
    {
        public string? BoxNo { get; set; } = "";
        public int  Code { get; set; } 
        public string? ScanNo { get; set; } = "";
        public int ScanQty { get; set; }
        public int ManualQty { get; set; } 
        public int ReceivedQty { get; set; } 
    }
}
