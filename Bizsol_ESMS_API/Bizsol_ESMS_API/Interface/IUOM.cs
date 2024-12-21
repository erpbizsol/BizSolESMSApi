using Bizsol_ESMS_API.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Bizsol_ESMS_API.Interface
{
    public interface IUOM
    {
        public abstract Task<IEnumerable<dynamic>> ShowUOM(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails);
        public abstract Task<IEnumerable<dynamic>> ShowUOMMasterByCode(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);
        public abstract Task<spOutputParameter> InsertUOM(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, tblUOMMaster model);
        public abstract Task<spOutputParameter> DeleteUOM(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code);


    }
}
