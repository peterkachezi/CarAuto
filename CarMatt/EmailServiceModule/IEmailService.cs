using CarMatt.Data.DTOs.ApplicationUserServiceModule;
using CarMatt.Data.DTOs.SubscriptionModule;

namespace CarMatt.EmailServiceModule
{
    public interface IEmailService
    {
        public bool SendAccountCreationEmailNotification(RegisterDTO registerDTO);

        public bool SendSubscriptionNotification(SubscriptionDTO  subscriptionDTO);
    }
}