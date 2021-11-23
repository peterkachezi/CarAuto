using CarMatt.Data.DTOs.ApplicationUserServiceModule;

namespace CarMatt.EmailServiceModule
{
    public interface IEmailService
    {
        public bool SendAccountCreationEmailNotification(RegisterDTO registerDTO);
    }
}