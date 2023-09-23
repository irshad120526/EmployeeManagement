using Employee.Model;
using EmployeeAPI.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalInfoController : Controller
    {
        private readonly IAdditionalInfoRepository _additionalInfoRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="additionalInfoRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AdditionalInfoController(IAdditionalInfoRepository additionalInfoRepository)
        {
            _additionalInfoRepository = additionalInfoRepository??throw new ArgumentNullException(nameof(additionalInfoRepository));
        }

        /// <summary>
        /// Get Employee's All Information By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAdditionalInfoById")]
        public async Task<IActionResult> GetAdditionalInfoById(int Id)
        {
            try
            {
                return Ok(await _additionalInfoRepository.GetAdditionalInfoById(Id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Get Only Additional Information By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInfoById")]
        public async Task<IActionResult> GetInfoById(int Id)
        {
            try
            {
                return Ok(await _additionalInfoRepository.GetInfoById(Id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Add Employee's Additional Information
        /// </summary>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAdditionalInfo")]
        public async Task<IActionResult> AddAdditionalInfo(AdditionalInfo additionalInfo)
        {
            try
            {
                return Ok(await _additionalInfoRepository.InsertAdditionalInfo(additionalInfo));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Update Employee's Additional Information
        /// </summary>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAdditionalInfo")]
        public async Task<IActionResult> UpdateAdditionalInfo(AdditionalInfo additionalInfo)
        {
            try
            {
                return Ok(await _additionalInfoRepository.UpdateAdditionalInfo(additionalInfo));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Employee's Additional Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAdditionalInfoById")]
        public async Task<IActionResult> DeleteAdditionalInfo(int id)
        {
            try
            {
                return Ok(await _additionalInfoRepository.DeleteAdditionalInfo(id));
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return NotFound();
            }
        }
    }
}
