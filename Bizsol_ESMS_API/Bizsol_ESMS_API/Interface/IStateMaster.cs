using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IStateMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowState(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowStateByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertState(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblStateMaster model);
        public abstract Task<spOutputParameter> DeleteState(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
