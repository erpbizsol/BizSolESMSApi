using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IDispatchOrder
    {
        public abstract Task<IEnumerable<dynamic>> GetDispatchOrderList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<VM_ShowDispatchOrder> GetDispatchOrderByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveDispatchOrderEntry(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_SaveDispatchOrder model, int UserMaster_Code);
        public abstract Task<dynamic> DeleteDispatchOrder(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetClientWiseOrderNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string ClientName);
        public abstract Task<IEnumerable<dynamic>> GetItemDetailByOrderNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string OrderNo);
        public abstract Task<IEnumerable<dynamic>> GetClientWiseShowOrder(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string ClientName);
        public abstract Task<VM_OrderMasterForShow> GetOrderDetailsForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> ScanItemForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanDispatch Dispatch);
        public abstract Task<dynamic> ManualItemForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanDispatch Dispatch);

    }
}
