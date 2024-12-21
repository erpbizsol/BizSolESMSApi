using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ICategory
    {
        public abstract Task<IEnumerable<dynamic>> ShowCategory(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowCategoryByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertCategory(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblCategoryMaster model);
        public abstract Task<spOutputParameter> DeleteCategory(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
