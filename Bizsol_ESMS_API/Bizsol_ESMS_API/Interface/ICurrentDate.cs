using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ICurrentDate
    {
        public abstract Task<IEnumerable<dynamic>> GetCurrentDate(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
    }
}
