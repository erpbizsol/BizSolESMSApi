namespace Bizsol_ESMS_API.Model
{
    public class tblTATReport
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public string? Date { get; set; } = "";
        public string? IsCheck { get; set; } = "N";
        public int UserMaster_Code { get; set; }
    }
}
