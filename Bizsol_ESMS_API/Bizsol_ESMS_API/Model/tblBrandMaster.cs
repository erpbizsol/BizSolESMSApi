namespace Bizsol_ESMS_API.Model
{
    public class tblBrandMaster
    {
        public int Code { get; set; } = 0;
        public string? BrandName { get; set; }
        public string PicklistNo { get; set; }
        public string BarcodeType { get; set; }
        public string ImportFormat { get; set; } = "";
    }
}
