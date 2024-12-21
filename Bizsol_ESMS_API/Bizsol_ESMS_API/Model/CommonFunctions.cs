
using Bizsol_ESMS_API.Model;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Bizsol_ESMS_API.Model
{
    public static class CommonFunctions
    {
        public static BizsolESMSConnectionDetails InitializeERPConnection(HttpContext httpContext)
        {

            StringValues AutJsonKey = "";
            BizsolESMSConnectionDetails bizsolESMSConnectionDetails = new BizsolESMSConnectionDetails();

            if (httpContext.Request.Headers.TryGetValue("Auth-Key", out AutJsonKey))
            {
                bizsolESMSConnectionDetails = JsonConvert.DeserializeObject<BizsolESMSConnectionDetails>(AutJsonKey);
            }

            return bizsolESMSConnectionDetails;
        }
    }
}
