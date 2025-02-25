using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IOrder
    {
        public abstract Task<IEnumerable<dynamic>> ShowOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<VM_OrderMasterForShow> ShowOrderMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> InsertOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_OrderMaster model,int UserMaster_Code);
        public abstract Task<dynamic> DeleteOrderMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        public abstract Task<IEnumerable<dynamic>> ClientWiseRate(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string ClientName, string ItemName);
        public abstract Task<dynamic> ImportOrder(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOrder ImportOrder);
        public abstract Task<dynamic> ImportOrderForTemp(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportOrder ImportOrder);
        public abstract Task<IEnumerable<dynamic>> ShowBoxNumber(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string PickListNo);

    }
}
