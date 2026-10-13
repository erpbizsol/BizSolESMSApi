namespace Bizsol_ESMS_API.Model
{
    public class tblPriceListDetail
    {
        public int ItemMaster_Code { get; set; } = 0;
        public string? ItemCode { get; set; } = "";
        public string? ItemName { get; set; } = "";
        public string? ItemBarCode { get; set; } = "";
        public double Rate { get; set; } = 0;
    }
}
