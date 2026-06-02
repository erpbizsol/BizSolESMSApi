namespace Bizsol_ESMS_API.Model
{
    public class tblInvoiceMasterSave
    {
        public int Code { get; set; }
        public Dictionary<string, object>? JsonHeader { get; set; }
        public List<Dictionary<string, object>>? JsonLines { get; set; }
    }
}
