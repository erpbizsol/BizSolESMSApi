namespace Bizsol_ESMS_API.Model
{
    public class tblTATConfiguration
    {
        public int Code { get; set; } = 0;
        public string? Division { get; set; }
        public string? CityMaster_Code { get; set; }
        public int Invoice { get; set; } 
        public int MinKM { get; set; } 
        public int MaxKM { get; set; } 
        public int Dispatch { get; set; } 
        public int Packing { get; set; }
        public int Delivered { get; set; }
        public int UserMaster_Code { get; set; }
     
    }
}
