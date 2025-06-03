namespace Bizsol_ESMS_API.Model
{
    public class tblOrder
    {
        public int Code { get; set; } = 0;
        public string? OrderNo { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public string? ClientName { get; set; } = "";
        public string? BuyerPONo { get; set; } = "";
        public DateTime BuyerPODate { get; set; }
        public string? Remark { get; set; } = "";
        

    }
}
