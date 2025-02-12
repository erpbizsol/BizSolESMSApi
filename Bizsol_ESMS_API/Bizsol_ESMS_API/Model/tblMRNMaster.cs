namespace Bizsol_ESMS_API.Model
{
    public class tblMRNMaster
    {
        public int Code { get; set; }
        public string? MRNNo { get; set; } = "";
        public DateTime? MRNDate { get; set; }
        public string? VendorName { get; set; } = "";
        public string? ChallanNo { get; set; } = "";
        public DateTime? ChallanDate { get; set; }
        public string? VehicleNo { get; set; } = "";
        public string? PickListNo { get; set; } = "";
    }
}
