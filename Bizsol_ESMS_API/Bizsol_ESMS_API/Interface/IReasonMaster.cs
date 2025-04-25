using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IReasonMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetReasonMasterList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);

    }
}
