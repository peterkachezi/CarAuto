using CarMatt.Data.DTOs.SubscriptionModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.SubscriptionModule
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDTO> Create(SubscriptionDTO subscriptionDTO);
        Task<bool> Delete(Guid Id);
        Task<SubscriptionDTO> GetById(Guid Id);
        Task<List<SubscriptionDTO>> GetAll();
    }
}