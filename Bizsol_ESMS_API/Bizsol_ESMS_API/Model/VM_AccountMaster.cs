namespace Bizsol_ESMS_API.Model
{
    public class VM_AccountMaster
    {
        public IEnumerable<tblCustomerType> AccountMaster { get; set; }
        public IEnumerable<tblAddressMater> AccountAddress{ get; set; }
    }
}
