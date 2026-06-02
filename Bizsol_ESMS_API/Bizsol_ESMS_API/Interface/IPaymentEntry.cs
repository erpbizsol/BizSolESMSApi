using Bizsol_ESMS_API.Model;



namespace Bizsol_ESMS_API.Interface

{
    public interface IPaymentEntry

    {
        public abstract Task<IEnumerable<dynamic>> GetInvoiceDetailsByAccountMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int AccountMaster_Code);
        public abstract Task<dynamic> SavePaymentEntry(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_BillMaster vmBillMaster, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetPaymentMasterlist(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string FromDate, string ToDate, int AccountMaster_Code, string PaymentMode);
        public abstract Task<VM_BillMasterList> GetPaymentEntryByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<dynamic> DeletePaymentEntry(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetPendingInvoiceReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int AccountMaster_Code, string AsonDate);
        public abstract Task<dynamic> SavePaymentEntryAdjustment(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_BillMaster vmBillMaster, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetPaymentAdjustmentMasterlist(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string FromDate, string ToDate, int AccountMaster_Code, string PaymentMode);
        public abstract Task<VM_BillMasterList> GetPaymentEntryAdjustmentByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<dynamic> DeletePaymentEntryAdjustment(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code, int UserMaster_Code);
        public abstract Task<dynamic> GetPaymentModeList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);

    }
}

