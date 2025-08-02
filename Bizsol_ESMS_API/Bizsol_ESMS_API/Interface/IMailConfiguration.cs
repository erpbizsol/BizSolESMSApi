using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IMailConfiguration
    {
        public abstract Task<IEnumerable<dynamic>> ShowMailConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowMailConfigurationByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> DeleteMailConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<dynamic> SaveMailConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblMailConfiguration model);

    }
}
