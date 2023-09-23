using Dapper;
using Employee.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace EmployeeAPI.EmployeeRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IOptions<DataConfig> _connectionstring;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DepartmentRepository(IOptions<DataConfig> connectionString)
        {
            _connectionstring = connectionString;
        }

        /// <summary>
        /// Add New Department
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public async Task<Department> AddDepartment(Department dept)
        {
            Department department = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionstring.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "I");
                    param.Add("DepartmentName", dept.Department_Name);
                    param.Add("IsActive", dept.IsActvie);
                    department = await con.QueryFirstOrDefaultAsync<Department>("SP_CRUD_Department", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return department;
        }

        /// <summary>
        /// Delete Department By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDepartment(int id)
        {
            bool isDelete = false;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionstring.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "D");
                    param.Add("@Id", id);
                    param.Add("@IsActive", false);
                    var result = await con.QueryFirstOrDefaultAsync<int>("SP_CRUD_Department", param, commandType: CommandType.StoredProcedure);
                    if (result > 0) isDelete = true;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return isDelete;
        }

        /// <summary>
        /// Get Department By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentById(int id)
        {
            Department department = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionstring.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "GBI");
                    param.Add("@Id", id);
                    department = await con.QueryFirstOrDefaultAsync<Department>("SP_CRUD_Department", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return department;
        }

        /// <summary>
        /// Get All Department's List
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            List<Department> departments = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionstring.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "GA");
                    param.Add("@IsActive", true);
                    var data = await con.QueryAsync<Department>("SP_CRUD_Department", param, commandType: CommandType.StoredProcedure);
                    departments = data.ToList();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return departments;
        }

        /// <summary>
        /// Update Existing Department
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public async Task<Department> UpdateDepartment(Department dept)
        {
            Department department = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionstring.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "U");
                    param.Add("@Id", dept.Department_Id);
                    param.Add("@DepartmentName", dept.Department_Name);
                    department = await con.QueryFirstOrDefaultAsync<Department>("SP_CRUD_Department", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return department;
        }
    }
}
