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
        public int Assigned { get; set; }
        public string? AssignedText { get; set; }
        public string? CommittedDate { get; set; }
        public string? EstimatedTime { get; set; }
        public int WorkType { get; set; }
        public string? WorkTypeText { get; set; }
        public int Employee_Code { get; set; }
        public string? EmployeeName { get; set; }
    }
}
