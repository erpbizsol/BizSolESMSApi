namespace Bizsol_ESMS_API.Model
{
    public class tblImportMRNMaster
    {
        public List<Dictionary<string, object>> JsonData { get; set; }
        public string? VendorName { get; set; }
        public string? VehicleNo { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
