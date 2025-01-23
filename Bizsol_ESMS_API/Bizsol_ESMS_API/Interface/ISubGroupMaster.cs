using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ISubGroupMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowSubGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowSubGroupByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<IEnumerable<dynamic>> ShowSubGroupByGroupName(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string GroupName);
        public abstract Task<spOutputParameter> InsertSubGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblSubGroupMaster model,int UserMaster_Code);
        public abstract Task<spOutputParameter> DeleteSubGroup(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code,int UserMaster_Code);
    }
}
