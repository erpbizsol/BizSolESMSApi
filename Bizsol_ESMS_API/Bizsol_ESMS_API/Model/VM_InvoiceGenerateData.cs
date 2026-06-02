namespace Bizsol_ESMS_API.Model
{
    public class VM_InvoiceGenerateData
    {
        public IEnumerable<dynamic> InvoiceHeader { get; set; }
        public IEnumerable<dynamic> InvoiceLines { get; set; }
    }
}
