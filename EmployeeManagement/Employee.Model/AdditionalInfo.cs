using System.ComponentModel.DataAnnotations;

namespace Employee.Model
{
    public class AdditionalInfo
    {
        [Key]
        public int AI_Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Gender { get; set; }
        public string Marital_Status { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public int Employee_Id { get; set; }
        public bool IsActive { get; set; }
    }
}
