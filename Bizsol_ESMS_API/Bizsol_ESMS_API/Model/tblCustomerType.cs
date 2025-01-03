namespace Bizsol_ESMS_API.Model
{
    public class tblCustomerType
    {
        public int Code { get; set; } = 0;
        public string AccountName { get; set; }
        public string DisplayName { get; set; }
        public string PANNo { get; set; }
        public string IsClient { get; set; }
        public string IsVendor { get; set; }
        public string IsMSME { get; set; }

    }
}
