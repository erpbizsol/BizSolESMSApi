using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IWarehouse
    {
        public abstract Task<IEnumerable<dynamic>> ShowWarehouse(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowWarehouseMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertWarehouse(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblWarehouse model);
        public abstract Task<spOutputParameter> DeleteWarehouse(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
