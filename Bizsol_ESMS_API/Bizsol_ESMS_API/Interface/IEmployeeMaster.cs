using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IEmployeeMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowEmployeeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowEmployeeMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertEmployeeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblEmployeeMaster model, int UserMaster_Code);
        public abstract Task<dynamic> DeleteEmployeeMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }
}
