using CarMatt.Data.DTOs.FeedBackModule;
using CarMatt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarMatt.Data.Services.FeedBackModule
{
    public class FeedBackService : IFeedBackService
    {
        private readonly ApplicationDbContext context;


        public FeedBackService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<FeedBackDTO> Create(FeedBackDTO feedBackDTO)
        {
            try
            {


                var s = new FeedBack
                {
                    Id = Guid.NewGuid(),

                    Name = feedBackDTO.Name.Substring(0, 1).ToUpper() + feedBackDTO.Name.Substring(1).ToLower().Trim(),

                    PhoneNumber = feedBackDTO.PhoneNumber,

                    Email = feedBackDTO.Email.ToLower(),

                    Message = feedBackDTO.Message,

                    CreateDate = DateTime.Now,

                };

                context.FeedBacks.Add(s);

                await context.SaveChangesAsync();

                return feedBackDTO;
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
                bool result = false;

                var s = await context.FeedBacks.FindAsync(Id);

                if (s != null)
                {
                    context.FeedBacks.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<FeedBackDTO>> GetAll()
        {
            try
            {
                var feedback = await context.FeedBacks.ToListAsync();

                var feedbacks = new List<FeedBackDTO>();

                foreach (var item in feedback)
                {
                    var data = new FeedBackDTO
                    {
                        Id = item.Id,

                        Name = item.Name,

                        Email = item.Email,

                        PhoneNumber = item.PhoneNumber,

                        Message = item.Message,

                        CreateDate = item.CreateDate,
                    };

                    feedbacks.Add(data);
                }

                return feedbacks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<FeedBackDTO> GetById(Guid Id)
        {
            try
            {
                var carmake = await context.FeedBacks.FindAsync(Id);

                return new FeedBackDTO
                {
                    Id = carmake.Id,

                    Name = carmake.Name,

                    Email = carmake.Email,

                    PhoneNumber = carmake.PhoneNumber,

                    Message = carmake.Message,

                    CreateDate = carmake.CreateDate,
                                       
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<FeedBackDTO> Update(FeedBackDTO feedBackDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.FeedBacks.FindAsync(feedBackDTO.Id);

                    s.Name = feedBackDTO.Name.Substring(0, 1).ToUpper() + feedBackDTO.Name.Substring(1).ToLower().Trim();

                    s.Email = feedBackDTO.Email;

                    s.PhoneNumber = feedBackDTO.PhoneNumber;

                    transaction.Commit();
                }

                await context.SaveChangesAsync();

                return feedBackDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
