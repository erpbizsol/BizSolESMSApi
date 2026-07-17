namespace Bizsol_ESMS_API.Model
{
    public class tblLocationMaster
    {
        public int Code { get; set; }
        public string? LocationName { get; set; } = "";
        public string? Location { get; set; } = "";
        public string? LocationGroup { get; set; } = "";
        public string? Mode { get; set; } = "";
        public int WarehouseMaster_Code { get; set; }
    }
}
