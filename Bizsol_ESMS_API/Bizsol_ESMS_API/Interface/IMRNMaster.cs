using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IMRNMaster
    {
        public abstract Task<IEnumerable<dynamic>> GetMRNMasterList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<VM_MRNMasterList> GetMRNMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<dynamic> SaveMRNMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, VM_MRNMaster model,int UserMaster_Code);
        public abstract Task<dynamic> ImportMRNMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportMRNMaster ImportMRNMaster);
        public abstract Task<dynamic> ImportMRNMasterForTemp(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblImportMRNMaster ImportMRNMaster);
        public abstract Task<dynamic> DeleteMRNMaster(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code,int UserMaster_Code);
        public abstract Task<dynamic> GetRateByVendor(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails,string VendorName,string ItemName);
        public abstract Task<dynamic> BoxUnloading(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblBoxUnloading model);

    }
}
