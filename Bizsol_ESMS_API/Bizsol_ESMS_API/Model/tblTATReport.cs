namespace Bizsol_ESMS_API.Model
{
    public class tblTATReport
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public string? Month { get; set; } = "";
        public string? Year { get; set; } = "";
        public string? IsCheck { get; set; } = "N";
        public int UserMaster_Code { get; set; }
    }
}
