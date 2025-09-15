using Bizsol_ESMS_API.Model;
using Bizsol_ESMS_API.Service;

namespace Bizsol_ESMS_API.Interface
{
    public interface IReport
    {
        public abstract Task<IEnumerable<dynamic>> GetLocationReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string Templete);
        public abstract Task<VM_UPIID_Details> GetUPIIDReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string UPI_ID);
        public abstract Task<IEnumerable<dynamic>> GetReportType(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string ModuleDesp);
        public abstract Task<IEnumerable<dynamic>> GetStockLedgerList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStockLedger StockLedger);
        public abstract Task<IEnumerable<dynamic>> GetStockAuditMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string FromDate,string ToDate,string Mode);
        public abstract Task<IEnumerable<dynamic>> SaveGoldenCruiserQRDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblGoldenCruiserQRDetails StockLedger);
        public abstract Task<IEnumerable<dynamic>> GetGoldenCruiserQRDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ResetGoldenCruiserQRDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
    }
}
