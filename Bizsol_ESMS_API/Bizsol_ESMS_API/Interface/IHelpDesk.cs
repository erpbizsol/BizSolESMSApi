using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IHelpDesk
    {
        public abstract Task<IEnumerable<dynamic>> ShowHelpdesk(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowHelpdeskByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> DeleteHelpdesk(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<dynamic> SaveHelpdesk(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblHelpdesk model);
        public abstract Task<dynamic> CompleteTicket(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);

    }
}
