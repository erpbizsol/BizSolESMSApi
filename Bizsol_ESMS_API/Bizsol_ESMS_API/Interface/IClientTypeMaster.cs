using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IClientTypeMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowClientTypeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowClientTypeMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertClientTypeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblClientTypeMaster model, int UserMaster_Code);
        public abstract Task<spOutputParameter> DeleteClientTypeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
