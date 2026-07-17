using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IScanToBill
    {
        public abstract Task<dynamic> SaveItemScanToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanToBill Dispatch);
        public abstract Task<dynamic> ManuaItemScanToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanDispatch Dispatch);
        public abstract Task<VM_OrderMasterForShow> GetDetailsItemScanToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<dynamic> AddItemScanToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblAddItemScanToBill AddItemScanToBill);
        public abstract Task<dynamic> SaveManualRateAndQtySacnToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblManualRateAndQty Dispatch);
        public abstract Task<dynamic> DeleteItemFormScanToBill(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);

    }
}
