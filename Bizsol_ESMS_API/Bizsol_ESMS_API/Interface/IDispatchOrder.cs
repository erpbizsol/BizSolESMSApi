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
        public abstract Task<IEnumerable<dynamic>> GetClientWiseShowOrder(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string Mode);
        public abstract Task<VM_OrderMasterForShow> GetOrderDetailsForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code,string Mode,int DispatchMaster_Code);
        public abstract Task<dynamic> ScanItemForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanDispatch Dispatch , string Mode);
        public abstract Task<dynamic> ManualItemForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblScanDispatch Dispatch,string Mode);
        public abstract Task<dynamic> GetMarkasCompeteByOrderNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<IEnumerable<dynamic>> GetDispatchReport(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<dynamic> UpdateBoxNo(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblUpdateBoxNo UpdateBoxNo, string Mode);
        public abstract Task<dynamic> DeleteDispatchItemQty(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveManualRateAndQty(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblManualRateAndQty Dispatch);
        public abstract Task<IEnumerable<dynamic>> GetDispatchQRDetail(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int Code);
        public abstract Task<IEnumerable<dynamic>> GetDispatchValidationDetail(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetDispatchValidationViewData(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int Code);
        public abstract Task<IEnumerable<dynamic>> GetDispatchValidationEditData(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int Code);
        public abstract Task<IEnumerable<dynamic>> GetGatePassData(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string VehicleNo, string Date);
        public abstract Task<IEnumerable<dynamic>> GetVehicleNoForDispatch(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> SaveDispatchBoxValidation(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblDispatchBoxValidation Dispatch);
        public abstract Task<IEnumerable<dynamic>> GetOrderPackedDetail(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string Date,string OrderStatus);
        public abstract Task<dynamic> GetTotalLineOfPart(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,int OrderMaster_Code);
        public abstract Task<IEnumerable<dynamic>> GetPackedOrderNoList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> GetOrderDetailsDataByCodes(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string Codes);
        public abstract Task<dynamic> SaveManualDispatchBoxValidation(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblDispatchBoxValidation Dispatch);

    }
}
