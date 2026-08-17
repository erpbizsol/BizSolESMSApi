namespace Bizsol_ESMS_API.Model
{
    public class CreateTicketResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TicketCode { get; set; }
        public string? TicketNo { get; set; }
        public bool EmailSent { get; set; }
        public int AttachmentCount { get; set; }
        public bool Assigned { get; set; }
        public bool AssignEmailSent { get; set; }
    }
}
