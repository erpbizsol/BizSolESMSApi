using Bizsol_ESMS_API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bizsol_ESMS_API.Interface
{
    public interface IHSNMaster
    {
        Task<IEnumerable<dynamic>> ShowHSN(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        Task<IEnumerable<dynamic>> ShowHSNMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        Task<spOutputParameter> InsertHSN(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblHSNMaster model, int UserMaster_Code);
        Task<spOutputParameter> DeleteHSN(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
