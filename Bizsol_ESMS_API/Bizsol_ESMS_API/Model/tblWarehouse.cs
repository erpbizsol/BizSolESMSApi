namespace Bizsol_ESMS_API.Model
{
    public class tblWarehouse
    {
        public int Code { get; set; } = 0;
        public string WarehouseName { get; set; }
        public string WarehouseType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string DisplayName { get; set; }
        public string Pin { get; set; }
        public string City { get; set; }
        public string GSTIN { get; set; }
        public string DefaultWarehouse { get; set; } = "N";
    }
}