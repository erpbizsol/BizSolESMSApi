namespace Bizsol_ESMS_API.Model
{
    public class tblImportOrder
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public string? ClientName { get; set; }
        public string? ClientType { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
