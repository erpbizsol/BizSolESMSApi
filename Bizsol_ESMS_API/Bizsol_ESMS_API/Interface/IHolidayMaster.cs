using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IHolidayMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetHolidayMasterList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetHolidayMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveHolidayMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblHolidayMaster model);
        public abstract Task<dynamic> DeleteHolidayMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);

    }
}
