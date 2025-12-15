using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ILogin
    {
        public abstract Task<dynamic> GetIsActiveDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int UserMaster_Code,string IPAddress);
        public abstract Task<dynamic> GetCountIsActiveUser(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> UpdateIsActiveUser(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int UserMaster_Code,string IsActive,string IPAddress);
        public abstract Task<dynamic> GetFixParameter(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
    }
}
