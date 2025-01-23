using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IPrefixConfiguration
    {
        public abstract Task<IEnumerable<dynamic>> GetPrefixConfigurationList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> SavePrefixConfiguration(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,tblPrefixConfiguration model);

    }
}
