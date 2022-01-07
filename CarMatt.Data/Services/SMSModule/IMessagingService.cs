using CarMatt.Data.DTOs.ApplicationUserServiceModule;
using CarMatt.Data.DTOs.FeedBackModule;
using CarMatt.Data.DTOs.InquiryModule;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<FeedBackDTO> FeedBackSMSAlert(FeedBackDTO feedBackDTO);

        Task<InquiryDTO> InquirySMSAlert(InquiryDTO inquiryDTO);
        Task<RegisterDTO> usersAccount(RegisterDTO registerDTO);
    }
}