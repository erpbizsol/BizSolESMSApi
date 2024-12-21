namespace Bizsol_ESMS_API.Model
{
    public class tblUOMMaster
    {
        public int Code { get; set; }
        public string? UOMName { get; set; } = "";
        public int DigitAfterDecimal { get; set; } =0;
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; } = 0;
        //public string? Status { get; set; } = "N";
    }
}
