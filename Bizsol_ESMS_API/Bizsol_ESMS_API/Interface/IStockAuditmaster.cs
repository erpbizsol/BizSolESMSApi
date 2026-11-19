using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IStockAuditmaster
    {
        public abstract Task<IEnumerable<dynamic>> GetStockAuditmasterList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ScanStockAuditmaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStockAuditmaster StockAuditmaster);
        public abstract Task<dynamic> CompleteStockAuditmaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code, int UserMaster_Code);
    }
}
