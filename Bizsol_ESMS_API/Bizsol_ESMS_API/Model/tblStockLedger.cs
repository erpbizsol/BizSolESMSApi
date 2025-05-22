namespace Bizsol_ESMS_API.Model
{
    public class tblStockLedger
    {
        public string? GroupMaster { get; set; }="";
        public string? SubGroupMaster { get; set; }="";
        public int ItemMaster_Code { get; set; }
        public string? FromDate { get; set; }="";
        public string? ToDate { get; set; } = "";
        public string? ReportType { get; set; } = "";
    }
}
