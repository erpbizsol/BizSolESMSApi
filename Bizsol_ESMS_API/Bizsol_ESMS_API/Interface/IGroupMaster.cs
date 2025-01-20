using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IGroupMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowGroupByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblGroupMaster model,int UserMaster_Code);
        public abstract Task<spOutputParameter> DeleteGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
