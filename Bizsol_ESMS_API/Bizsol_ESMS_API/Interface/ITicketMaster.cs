using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Http;

namespace Bizsol_ESMS_API.Interface
{
    public interface ITicketMaster
    {
        public abstract Task<CreateTicketResponse> CreateTicket(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,CreateTicketRequest request,IEnumerable<IFormFile>? files);
        public abstract Task<AttechedFileCheck[]> AttechedFileChecks(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string companyCode,long ticketNo);
    }
}
