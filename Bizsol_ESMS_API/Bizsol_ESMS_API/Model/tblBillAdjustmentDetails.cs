namespace Bizsol_ESMS_API.Model
{
    public class tblBillAdjustmentDetails
    {
        public int Code { get; set; }
        public int BillMaster_Code { get; set; }
        public int InvoiceMaster_Code { get; set; }
        public double PaymentAmount { get; set; }
    }
}
