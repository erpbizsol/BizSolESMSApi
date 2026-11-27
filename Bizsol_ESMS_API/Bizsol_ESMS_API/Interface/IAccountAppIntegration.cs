using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IAccountAppIntegration
    {
        public abstract Task<IEnumerable<dynamic>> GetTallybusyTempletConfigrution(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetAccountAppIntegrationTemplateData(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string template, string fromDate, string toDate);
        public abstract Task<IEnumerable<dynamic>> SyncToSqlServer(BizsolESMSConnectionDetails _bizsolESMSConnectionDetails, string template, string templateName, string fromDate, string toDate, List<string> codes,int UserCode);
    }
}
