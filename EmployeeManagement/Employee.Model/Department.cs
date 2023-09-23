using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;

namespace Employee.Model
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
        public bool IsActvie { get; set; }
    }
}
