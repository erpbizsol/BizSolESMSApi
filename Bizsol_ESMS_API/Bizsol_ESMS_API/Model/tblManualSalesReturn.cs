namespace Bizsol_ESMS_API.Model
{
    public class tblManualSalesReturn
    {
            public int Code { get; set; }
            public int ClientMasterCode { get; set; }
            public string? ScanNo { get; set; }
            public int UserMaster_Code { get; set; }
            public int WarehouseMaster_Code { get; set; }
            public int ReasonMaster_Code { get; set; }
    }
}
