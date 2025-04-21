using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ILocationMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowLocationMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowLocationMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertLocationMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblLocationMaster model, int UserMaster_Code);
        public abstract Task<dynamic> CreateLocationFromItemMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblLocationMaster model, int UserMaster_Code, string IsCheckExists);
        public abstract Task<spOutputParameter> DeleteLocationMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
