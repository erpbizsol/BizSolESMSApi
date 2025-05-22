namespace Bizsol_ESMS_API.Model
{
    public class tblSalesReturn
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public int ClientMaster_Code { get; set; }
        public int ReasonMaster_Code { get; set; }
        public string? OrderNo { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
