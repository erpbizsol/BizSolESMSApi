namespace Bizsol_ESMS_API.Model
{
    public class tblBillMaster
    {
        public int Code { get; set; }
        public int EntryNo { get; set; }
        public string? EntryDate { get; set; } = "";
        public int AccountMaster_Code { get; set; }
        public string? PaymentMode { get; set; } = "";
        public string? Refno { get; set; } = "";
        public double Amount { get; set; }
        public double AdvanceAmount { get; set; }
        public string? Narration { get; set; } = "";
    }
}
