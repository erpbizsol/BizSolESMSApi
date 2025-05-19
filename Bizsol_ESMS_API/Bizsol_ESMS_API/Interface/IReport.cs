using Bizsol_ESMS_API.Model;
using Bizsol_ESMS_API.Service;

namespace Bizsol_ESMS_API.Interface
{
    public interface IReport
    {
        public abstract Task<IEnumerable<dynamic>> GetLocationReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string Templete);
        public abstract Task<IEnumerable<dynamic>> GetReportType(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string ModuleDesp);
        public abstract Task<IEnumerable<dynamic>> GetStockLedgerList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStockLedger StockLedger);
    }
}
