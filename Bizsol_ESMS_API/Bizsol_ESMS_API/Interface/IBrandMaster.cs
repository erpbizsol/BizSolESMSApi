using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IBrandMaster
    {
                
        public abstract Task<IEnumerable<dynamic>> ShowBrand(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowBrandByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertBrand(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblBrandMaster model);
        public abstract Task<spOutputParameter> DeleteBrand(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
