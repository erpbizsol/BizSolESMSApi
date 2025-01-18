using System;

namespace Bizsol_ESMS_API.Model
{
    public class tblDispatchOrder
    {
        public int? Code {  get; set; }
        public string? ChallanNo { get; set; } = "";
        public DateTime? ChallanDate { get; set; }
        public string? ClientName { get; set; } = "";
        public string? VehicleNo { get; set; } = "";
    }
}
