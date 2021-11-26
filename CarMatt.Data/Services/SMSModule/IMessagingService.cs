using CarMatt.Data.DTOs.FeedBackModule;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<FeedBackDTO> FeedBackSMSAlert(FeedBackDTO feedBackDTO);
    }
}