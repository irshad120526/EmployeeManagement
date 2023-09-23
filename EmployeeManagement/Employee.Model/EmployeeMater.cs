using System.ComponentModel.DataAnnotations;

namespace Employee.Model
{
    public class EmployeeMater
    {
        [Key]
        public int Employee_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public DateTime Joining_Date { get; set; }
        public int Department_Id { get; set; }
        public bool IsActive { get; set; }
    }

    
}
