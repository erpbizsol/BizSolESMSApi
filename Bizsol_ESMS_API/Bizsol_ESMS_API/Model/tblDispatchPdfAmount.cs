namespace Bizsol_ESMS_API.Model
{
    public class tblDispatchPdfAmount
    {
        public int DispatchMaster_Code { get; set; }
        public decimal Total { get; set; }
        public string? AddType { get; set; }
        public decimal AddValue { get; set; }
        public string? LessType { get; set; }
        public decimal LessValue { get; set; }
        public decimal NetAmount { get; set; }
        public string? IsManual { get; set; }
    }
}
