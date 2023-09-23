using Dapper;
using Employee.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace EmployeeAPI.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly IOptions<DataConfig> _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeRepository(IOptions<DataConfig> connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Delete Employee By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployee(int id)
        {
            bool isDelete = false;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    param.Add("@Mode", "D");
                    param.Add("@IsActive", false);
                    var result = await con.QueryFirstOrDefaultAsync<int>("SP_CRUD_Employee", param, commandType: CommandType.StoredProcedure);
                    if (result > 0) { isDelete = true; }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return isDelete;
        }

        /// <summary>
        /// Get Employee's Details By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeMater> GetEmployeeById(int id)
        {
            EmployeeMater? employee = null;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    param.Add("@Mode", "GBI");
                    var emp = await con.QueryFirstOrDefaultAsync<EmployeeMater>("SP_CRUD_Employee", param, commandType: CommandType.StoredProcedure);
                    if (emp != null && emp.Employee_Id > 0)
                    {
                        employee = emp;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return employee;
        }

        /// <summary>
        /// Get All Employee's List
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeData>> GetEmployees(int skip)
        {
            List<EmployeeData> employeesList = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "GA");
                    param.Add("@IsActive", true);
                    param.Add("@Skip", skip);
                    var data = await con.QueryAsync<EmployeeData>("SP_CRUD_Employee", param, commandType: CommandType.StoredProcedure);
                    employeesList = data.ToList();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return employeesList;
        }

        /// <summary>
        /// Add New Employee
        /// </summary>
        /// <param name="employeeMater"></param>
        /// <returns></returns>
        public async Task<EmployeeMater> InsertEmployee(EmployeeMater employeeMater)
        {
            EmployeeMater employee = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "I");
                    param.Add("@EmpName", employeeMater.Emp_Name);
                    param.Add("@Designation", employeeMater.Designation);
                    param.Add("@Address", employeeMater.Address);
                    param.Add("@Salary", employeeMater.Salary);
                    param.Add("@JoiningDate", employeeMater.Joining_Date);
                    param.Add("@DepartmentId", employeeMater.Department_Id);
                    param.Add("@IsActive", employeeMater.IsActive);
                    employee = await con.QueryFirstOrDefaultAsync<EmployeeMater>("SP_CRUD_Employee", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return employee;
        }

        /// <summary>
        /// Update Existing Employee
        /// </summary>
        /// <param name="employeeMater"></param>
        /// <returns></returns>
        public async Task<EmployeeMater> UpdateEmployee(EmployeeMater employeeMater)
        {
            EmployeeMater employee = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "U");
                    param.Add("@Id", employeeMater.Employee_Id);
                    param.Add("@EmpName", employeeMater.Emp_Name);
                    param.Add("@Designation", employeeMater.Designation);
                    param.Add("@Address", employeeMater.Address);
                    param.Add("@Salary", employeeMater.Salary);
                    param.Add("@JoiningDate", employeeMater.Joining_Date);
                    param.Add("@DepartmentId", employeeMater.Department_Id);
                    employee = await con.QueryFirstOrDefaultAsync<EmployeeMater>("SP_CRUD_Employee", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return employee;
        }
    }
}
