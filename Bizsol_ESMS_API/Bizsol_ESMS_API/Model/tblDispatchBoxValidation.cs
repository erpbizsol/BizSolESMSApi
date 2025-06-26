namespace Bizsol_ESMS_API.Model
{
    public class tblDispatchBoxValidation
    {
        public int Code { get; set; }
        public string? BoxNo { get; set; }
        public string? VehicleNo { get; set; }
        public string? InvoiceNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverContactNo { get; set; }
        public decimal LorryMeter { get; set; }
        public int UserMaster_Code { get; set; }
    }
}
