using CarMatt.Data.DTOs.SubscriptionModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.SubscriptionModule
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext context;

        public SubscriptionService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<SubscriptionDTO> Create(SubscriptionDTO subscriptionDTO)
        {
            try
            {
                var s = new Subscription
                {
                    Id = Guid.NewGuid(),

                    Email = subscriptionDTO.Email,

                    CreateDate = DateTime.Now,

                };

                context.Subscriptions.Add(s);

                await context.SaveChangesAsync();

                return subscriptionDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool results = false;

                var s = await context.Subscriptions.FindAsync();

                if (s != null)
                {
                    context.Subscriptions.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<SubscriptionDTO>> GetAll()
        {
            try
            {
                var subscription = await context.Subscriptions.ToListAsync();

                var subscriptions = new List<SubscriptionDTO>();

                foreach (var item in subscription)
                {
                    var data = new SubscriptionDTO
                    {
                        Id = item.Id,

                        Email = item.Email,

                        CreateDate = item.CreateDate,
                    };

                    subscriptions.Add(data);
                }

                return subscriptions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<SubscriptionDTO> GetById(Guid Id)
        {
            try
            {
                var subscription = await context.Subscriptions.FindAsync(Id);

                return new SubscriptionDTO
                {
                    Id = subscription.Id,

                    Email = subscription.Email,

                    CreateDate = subscription.CreateDate,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

    }
}
