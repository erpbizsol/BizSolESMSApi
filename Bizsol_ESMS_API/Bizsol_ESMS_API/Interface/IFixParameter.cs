using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IFixParameter
    {
        Task<dynamic> VerifyFixParameterConfigAccess(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int userMasterCode, string? password);
        Task<dynamic> SaveFixParameter(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int userMasterCode, string password, string jsonData);
    }
}
