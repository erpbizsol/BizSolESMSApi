namespace Bizsol_ESMS_API.Model
{
    public class tblImportLocation
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public int WarehouseMaster_Code { get; set; }
        public string? InsertNewItem { get; set; }
        public string? InsertNewLocation { get; set; }
    }
}
