namespace Bizsol_ESMS_API.Model
{
    public class tblRetailerReplacementIn
    {
        public int Code { get; set; }
        public string? EntryNo { get; set; } = "";
        public string? EntryDate { get; set; } = "";
        public int AccountMaster_Code { get; set; }
        public int BrandMaster_Code { get; set; }
        public string? Remark { get; set; } = "";
    }
}
