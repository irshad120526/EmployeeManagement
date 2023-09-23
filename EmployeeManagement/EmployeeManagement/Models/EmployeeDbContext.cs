using Employee.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> option):base(option)
        {

        }

        public virtual DbSet<EmployeeMater> EmployeeMaster { get; set; }
    }
}
