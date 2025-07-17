namespace Bizsol_ESMS_API.Model
{
    public class tblStockAuditConfig
    {
        public int Code { get; set; }
        public int CategoryMaster_Code { get; set; }
        public string? Value { get; set; }
        public int CycleCountDays { get; set; }
        public int PartLineCount { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
