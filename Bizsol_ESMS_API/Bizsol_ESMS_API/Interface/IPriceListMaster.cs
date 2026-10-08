using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IPriceListMaster
    {
        public abstract Task<IEnumerable<dynamic>> ShowPriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        public abstract Task<VM_PriceListMasterForShow> ShowPriceListMasterByCode(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        public abstract Task<VM_PriceListMasterForShow> ShowPriceListMasterByEffectiveDate(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string effectiveDate);
        public abstract Task<dynamic> ValidatePriceListEffectiveDate(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, string effectiveDate);
        public abstract Task<dynamic> SavePriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, VM_PriceListMaster model, int userMasterCode);
        public abstract Task<dynamic> DeletePriceListMaster(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code, int userMasterCode);
    }
}
