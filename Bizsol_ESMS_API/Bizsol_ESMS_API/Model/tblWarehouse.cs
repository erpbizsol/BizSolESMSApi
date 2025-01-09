namespace Bizsol_ESMS_API.Model
{
    public class tblWarehouse
    {
        public int Code { get; set; } = 0;
        public string WarehouseName {  get; set; }
        public string WarehouseType {  get; set; }
        public string Address {  get; set; }
        public int Pin {  get; set; }=0;
        public string City {  get; set; }
        public string GSTIN {  get; set; }
        public string DefaultWarehouse { get; set; } = "N";
        public string StoreWarehouse { get; set; } = "N";   
        public string InTransitwarehouse { get; set; } = "N";
    }
}
