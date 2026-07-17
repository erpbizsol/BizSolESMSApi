namespace Bizsol_ESMS_API.Model
{
    public class VM_DailyStockReport
    {
        public IEnumerable<dynamic> DailyOrderReport { get; set; }
        public IEnumerable<dynamic> LossOrderReport { get; set; }
        public IEnumerable<dynamic> DeadStock { get; set; }
        public IEnumerable<dynamic> MonthWiseSale { get; set; }
        public IEnumerable<dynamic> AverageTrunAround { get; set; }
        public IEnumerable<dynamic> TopCustomers { get; set; }
        public IEnumerable<dynamic> Employee { get; set; }
        public IEnumerable<dynamic> SaleLossOrder { get; set; }
        public IEnumerable<dynamic> SaleReturn { get; set; }
        public IEnumerable<dynamic> TatConfig { get; set; }
        public IEnumerable<dynamic> TatMaster { get; set; }
        public IEnumerable<dynamic> StockSummary { get; set; }
        public IEnumerable<dynamic> Top10MinimumOrderParty { get; set; }
        public IEnumerable<dynamic> Top10MaximumOrderParty { get; set; }
        public IEnumerable<dynamic> ReorderLevelData { get; set; }
    }
}
