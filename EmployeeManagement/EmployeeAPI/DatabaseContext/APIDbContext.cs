using Employee.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.DatabaseContext
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        public virtual DbSet<EmployeeMater> EmployeeMaster { get; set; }
        public virtual DbSet<Department> Department { get; set; }
    }
}
