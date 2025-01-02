using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface  ICity
    {
        public abstract Task<IEnumerable<dynamic>> ShowCity(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<dynamic> ShowCityByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertCity(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblCity model);
        public abstract Task<spOutputParameter> DeleteCity(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
    }
}
