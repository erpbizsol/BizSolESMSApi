namespace Bizsol_ESMS_API.Model
{
    public class tblOrderCancellationSave
    {
        public int OrderMaster_Code { get; set; }
        public int ReasonMaster_Code { get; set; }
        public string? Remark { get; set; }
        public List<OrderCancellationDetailLine>? Details { get; set; }
    }

    public class OrderCancellationDetailLine
    {
        public int OrderDetailMaster_Code { get; set; }
        public decimal CancelQty { get; set; }
    }
}
