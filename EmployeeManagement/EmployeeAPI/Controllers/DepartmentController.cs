using Employee.Model;
using EmployeeAPI.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository?? throw new ArgumentNullException(nameof(departmentRepository));
        }

        /// <summary>
        /// Get All Department's List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllDepartment")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartments());
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Get Department By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartmentById(id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Add New Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            try
            {
                return Ok(await _departmentRepository.AddDepartment(department));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Update Existing Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            try
            {
                return Ok(await _departmentRepository.UpdateDepartment(department));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Department By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteDepartmentById")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                return Ok(await _departmentRepository.DeleteDepartment(id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }
    }
}
