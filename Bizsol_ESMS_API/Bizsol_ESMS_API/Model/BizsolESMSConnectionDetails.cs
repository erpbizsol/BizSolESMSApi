
namespace Bizsol_ESMS_API.Model
{
    public class BizsolESMSConnectionDetails
    {

        public string? DefultMysqlTemp { get; set; } //= "Server=220.158.165.98;Port = 65448;database=esms_demo;user=sa;password=biz1981;Pooling=true;Min Pool Size=0;Max Pool Size=200;Connection Timeout=30;";
        public string? DefaultSQL { get; set; } //= "data source=208.91.198.59; initial catalog=bwin387_bizsol;uid=nbwin138;PWD=Amt*#050";
        //public string? ERPDMSDBConStr { get; set; }
        //public string? ERPDB_Name { get; set; }
        //public string? ERPMainDB_Name { get; set; }
        public int UserMaster_Code { get; set; }
        public string? AuthToken { get; set; } = "";
    }
}
