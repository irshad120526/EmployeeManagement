using Employee.Model;
using EmployeeAPI.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IMailService mailService;

        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok(true);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return Ok(false);
            }

        }
    }
}
