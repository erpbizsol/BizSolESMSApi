using System;
using System.Data;

namespace Bizsol_ESMS_API.Model
{
    public class tblUserMaster
    {
		public int Code { get; set; }
		public string UserID { get; set; } = "";
		public string UserName { get; set; } = "";
		public string Password { get; set; } = "";
        public string UserType { get; set; } = "";
        public int GroupMaster_Code { get; set; }
		public string Status { get; set; } = "";
        public string ShowClientInProductionReport { get; set; } = "";
        public int FixedParameter_Code { get; set; }
        public int CreatedBy { get; set; } 
        public DateTime CreateDate { get; set; }
		public string ChangePasswordForNextLogIn { get; set; } = "N";
        public string ShowRatesInQuotation { get; set; } = "Y";
        public byte[] UserImage { get; set; }
        public string UserMobileNo { get; set; } = "";
        public int DesignationMaster_Code { get; set; } 
        public int DefaultPage { get; set; } 
        public string UserLocation { get; set; } = "";
        public string LoginAllowFromSystem { get; set; } = "";
        public string OTPApplicable { get; set; } = "N";
        public DateTime InActiveDate { get; set; } 
        public int NoOfSessionAllowed { get; set; }
        public string IsBizSolUser { get; set; } = "N";
        public string EmailId { get; set; } = "";
        public int UserMaster_Code { get; set; }
    }   
}
