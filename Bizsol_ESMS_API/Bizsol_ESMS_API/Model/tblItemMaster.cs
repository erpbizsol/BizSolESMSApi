using Microsoft.AspNetCore.Http.HttpResults;

namespace Bizsol_ESMS_API.Model
{
    public class tblItemMaster
    {
        public int Code { get; set; } = 0;
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string ItemBarCode { get; set; } = "";
        public string UOMName { get; set; } = "";
        public string HSNCode { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string GroupName { get; set; } = "";
        public string SubGroupName { get; set; } = "";
        public string BrandName { get; set; } = "";
        public int ReorderLevel { get; set; } = 0;
        public int ReorderQty { get; set; } = 0;
        public string LocationName { get; set; }
        public string BoxPacking { get; set; } = "N";
        public int QtyInBox { get; set; } = 0;
        public string BatchApplicable { get; set; }
        public string MaintainExpiry { get; set; } 
    }
}
