using System.ComponentModel.DataAnnotations;

namespace Employee.Model
{
    public class AdditionalInfoData
    {
        [Key]
        public int AI_Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime Birth_Date { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Marital_Status { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public int Employee_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public DateTime Joining_Date { get; set; }
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
    }
}
