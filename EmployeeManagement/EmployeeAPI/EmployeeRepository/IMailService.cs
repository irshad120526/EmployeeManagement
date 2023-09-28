using Employee.Model;

namespace EmployeeAPI.EmployeeRepository
{
    public interface IMailService
    {
        public Task SendEmailAsync(MailRequest mailRequest);
    }
}
