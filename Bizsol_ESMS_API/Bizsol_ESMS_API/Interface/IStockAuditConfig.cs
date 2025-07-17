using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IStockAuditConfig
    {
        public abstract Task<IEnumerable<dynamic>> GetStockAuditConfigList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetStockAuditConfigByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveStockAuditConfig(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStockAuditConfig model);
        public abstract Task<dynamic> DeleteStockAuditConfig(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);

    }
}
