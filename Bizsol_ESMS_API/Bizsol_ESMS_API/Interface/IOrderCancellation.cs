using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IOrderCancellation
    {
        Task<IEnumerable<dynamic>> GetOrderCancellationList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        Task<IEnumerable<dynamic>> GetOrderCancellationLines(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        Task<IEnumerable<dynamic>> GetOrderCancellationHeader(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        Task<IEnumerable<dynamic>> GetOrderCancellationItems(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        Task<VM_OrderCancellationDetail> GetOrderCancellationDetail(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        Task<IEnumerable<dynamic>> GetOrderCancellationDispatch(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code);
        Task<dynamic> SaveOrderCancellation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, tblOrderCancellationSave model, int userMasterCode);
        Task<IEnumerable<dynamic>> GetReOpenOrderList(BizsolESMSConnectionDetails bizsolESMSConnectionDetails);
        Task<dynamic> ReOpenOrderCancellation(BizsolESMSConnectionDetails bizsolESMSConnectionDetails, int code, int userMasterCode);
    }
}
