using Microsoft.AspNetCore.Http.HttpResults;

namespace Bizsol_ESMS_API.Model
{
    public class tblItemMaster
    {
        public int Code { get; set; } = 0;
        public string ItemName { get; set; } 
        public string DisplayName { get; set; }
        public int ItemBarCode { get; set; } = 0;
        public string UOMName { get; set; } 
        public string HSNCode { get; set; }
        public string CategoryName { get; set; } 
        public string GroupName { get; set; } 
        public string SubGroupName { get; set; }
        public string BrandName { get; set; }
        public decimal MRP { get; set; }
        public int ReorderLevel { get; set; } = 0;
        public int ReorderQty { get; set; } = 0;
        public string LocationName { get; set; }
        public string BoxPacking { get; set; }
        public int QtyInBox { get; set; } = 0;


    }
}
