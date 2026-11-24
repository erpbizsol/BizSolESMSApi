using Bizsol_ESMS_API.Model;

namespace Bizsol_ESMS_API.Interface
{
    public interface IWarehouseStockTransferService
    {
        Task<IEnumerable<dynamic>> GetList(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, string? fromDate, string? toDate, int warehouseMasterCode, int UserMaster_Code);
        Task<WarehouseTransferDetailResponse> GetDetails(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
        Task<IEnumerable<dynamic>> ScanItem(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, WarehouseTransferScanModel model);
        Task<IEnumerable<dynamic>> UpdateHeader(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, WarehouseTransferHeaderModel model);
        Task<IEnumerable<dynamic>> CompleteTransfer(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, WarehouseTransferCompleteModel model);
        Task<IEnumerable<dynamic>> DeleteTransfer(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, WarehouseTransferCompleteModel model);
        Task<WarehouseTransferPrintResponse> GetPrintData(BizsolESMSConnectionDetails _BizsolESMSConnectionDetails, int code, int UserMaster_Code);
    }

    public class WarehouseTransferPrintResponse
    {
        public IEnumerable<dynamic> Header { get; set; } = new List<dynamic>();
        public IEnumerable<dynamic> Items { get; set; } = new List<dynamic>();
    }

    /// <summary>Complete a pending transfer (Action button in list).</summary>
    public class WarehouseTransferCompleteModel
    {
        public int Code { get; set; }
        public int UserMaster_Code { get; set; }
    }

    /// <summary>Scan request. Code = 0 -> new transfer (master is created on first scan).</summary>
    public class WarehouseTransferScanModel
    {
        public int Code { get; set; }
        public string? ScanNo { get; set; }
        public int UserMaster_Code { get; set; }
        public string? EntryDate { get; set; }                 // yyyy-MM-dd
        public int FromWarehouseMaster_Code { get; set; }
        public int ToWarehouseMaster_Code { get; set; }
        public string? RefNo { get; set; }
    }

    /// <summary>Update Ref No / Date of an existing transfer.</summary>
    public class WarehouseTransferHeaderModel
    {
        public int Code { get; set; }
        public int UserMaster_Code { get; set; }
        public string? EntryDate { get; set; }
        public string? RefNo { get; set; }
    }

    /// <summary>GetDetails response : header (Master) + scanned items grid (Detail).</summary>
    public class WarehouseTransferDetailResponse
    {
        public IEnumerable<dynamic> Master { get; set; } = new List<dynamic>();
        public IEnumerable<dynamic> Detail { get; set; } = new List<dynamic>();
    }
}
