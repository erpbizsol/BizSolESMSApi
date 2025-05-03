using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IOrder
    {
        public abstract Task<IEnumerable<dynamic>> ShowOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<VM_OrderMasterForShow> ShowOrderMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_OrderMaster model,int UserMaster_Code);
        public abstract Task<dynamic> DeleteOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> ClientWiseRate(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string ClientName, string ItemName);
        public abstract Task<dynamic> ImportOrder(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOrder ImportOrder);
        public abstract Task<dynamic> ImportOrderForTemp(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOrder ImportOrder);
        public abstract Task<IEnumerable<dynamic>> ShowBoxNumber(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string PickListNo);
        public abstract Task<IEnumerable<dynamic>> GetEmployeeList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int UserMaster_Code);
        public abstract Task<dynamic> ImportSalesReturn(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblSalesReturn SalesReturn);
        public abstract Task<dynamic> ImportSalesReturnForTemp(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblSalesReturn SalesReturn);
        public abstract Task<IEnumerable<dynamic>> ShowSalesReturnMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowSalesReturnMasterDetail(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int Code);
        public abstract Task<dynamic> SaveSalesReturnScanDetail(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanSalesReturn SalesReturn);
        public abstract Task<dynamic> UpdateReasonMaster_CodeInSaleReturn(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int Code,int ReasonMaster_Code);
        public abstract Task<dynamic> ImportOpeningBalance(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOpeningBalance OpeningBalance);
        public abstract Task<dynamic> ImportOpeningBalanceForTemp(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOpeningBalance OpeningBalance);
        public abstract Task<IEnumerable<dynamic>> GetStockAuditList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ScanStockAudit(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStockAudit StockAudit);
        public abstract Task<dynamic> ManualStockAudit(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code,int UserMaster_Code);

    }
}
