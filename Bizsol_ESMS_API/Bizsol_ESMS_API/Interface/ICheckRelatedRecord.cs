using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface ICheckRelatedRecord
    {
        public abstract Task<IEnumerable<dynamic>> ICheckRelatedRecord(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int p_CodeValue, string p_TableName);
    }
}
