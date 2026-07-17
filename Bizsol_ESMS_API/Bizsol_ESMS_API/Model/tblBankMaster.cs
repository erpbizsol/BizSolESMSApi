namespace Bizsol_ESMS_API.Model
{
    public class tblBankMaster
    {
        public int Code { get; set; } = 0;
        public string? BankName { get; set; } = "";
        public string? AccountNo { get; set; } = "";
        public string? IFSCCode { get; set; } = "";
        public string? Branch { get; set; } = "";
        public string? Type { get; set; } = "";
        public string DefaultCheck { get; set; } = "N";
    }
}
