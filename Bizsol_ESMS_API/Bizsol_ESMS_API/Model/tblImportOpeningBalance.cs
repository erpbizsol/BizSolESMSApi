namespace Bizsol_ESMS_API.Model
{
    public class tblImportOpeningBalance
    {
            public List<Dictionary<string, object>> JsonData { get; set; }
            public int UserMaster_Code { get; set; }
            public string? WarehouseName { get; set; } = "";
    }
}
