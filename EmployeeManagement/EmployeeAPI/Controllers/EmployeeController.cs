using Employee.Model;
using EmployeeAPI.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        /// <summary>
        /// Get All Employee's List
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllEmployee")]
        public async Task<IActionResult> GetEmployee(int skip)
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees(skip));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Get Employee's Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployeeById(Id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Add New Employee Details
        /// </summary>
        /// <param name="employeeMater"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeMater employeeMater)
        {
            try
            {
                return Ok(await _employeeRepository.InsertEmployee(employeeMater));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Update Existing Employee Details
        /// </summary>
        /// <param name="employeeMater"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeMater employeeMater)
        {
            try
            {
                return Ok(await _employeeRepository.UpdateEmployee(employeeMater));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Employee Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteEmployeebyId")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                return Ok(await _employeeRepository.DeleteEmployee(Id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return Ok(false);
            }
        }
    }
}
