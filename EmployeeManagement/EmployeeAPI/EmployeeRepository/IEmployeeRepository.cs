using Employee.Model;

namespace EmployeeAPI.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeData>> GetEmployees(int skip);
        Task<EmployeeMater> GetEmployeeById(int id);
        Task<EmployeeMater> InsertEmployee(EmployeeMater employeeMater);
        Task<EmployeeMater> UpdateEmployee(EmployeeMater employeeMater);
        Task <bool> DeleteEmployee(int id);
    }
}
