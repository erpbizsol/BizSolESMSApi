using Bizsol_ESMS_API.Model;
using Bizsol_ESMS_API.Service;

namespace Bizsol_ESMS_API.Interface
{
    public interface IReport
    {
        public abstract Task<IEnumerable<dynamic>> GetLocationReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string Templete);
       
   
    }
}
