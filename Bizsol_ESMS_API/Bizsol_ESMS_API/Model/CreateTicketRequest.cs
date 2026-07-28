namespace Bizsol_ESMS_API.Model
{
    public class CreateTicketRequest
    {
        public string? CompanyCode { get; set; }
        public string? TicketType { get; set; }
        public string? TicketNo { get; set; }
        public string? Priority { get; set; }
        public string? ProjectClient { get; set; }
        public string? LogDate { get; set; }
        public string? Module { get; set; }
        public string? RaisedBy { get; set; }
        public string? ContactNo { get; set; }
        public string? ContactEMail { get; set; }
        public string? Source { get; set; }
        public string? Description { get; set; }
        public int CreateTicketBy { get; set; }
        public string? TestedBy { get; set; }
        public string? UserModuleMaster_Code { get; set; }
    }
}
