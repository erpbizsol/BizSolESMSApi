using Bizsol_ESMS_API.Model;
using Bizsol_ESMS_API.Service;

namespace Bizsol_ESMS_API.Interface
{
    public interface ITATConfiguration
    {
        public abstract Task<IEnumerable<dynamic>> ShowTATConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowTATConfigurationByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> DeleteTATConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<dynamic> SaveTATConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblTATConfiguration model);

    }
}
