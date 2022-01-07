using CarMatt.Data.DTOs.InquiryModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarMatt.Data.Services.Utils.Enumerations;

namespace CarMatt.Data.Services.InquiryModule
{
    public class InquiryService : IInquiryService
    {
        private readonly ApplicationDbContext context;

        public InquiryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<InquiryDTO> Create(InquiryDTO inquiryDTO)
        {
            try
            {
                var s = new Inquiry
                {
                    Id = Guid.NewGuid(),

                    Name = inquiryDTO.Name,

                    Message = inquiryDTO.Message,

                    PhoneNumber = inquiryDTO.PhoneNumber,

                    Status = 0,

                    CreateDate = DateTime.Now,
                };

                context.Inquiries.Add(s);

                await context.SaveChangesAsync();

                return inquiryDTO;
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

                var s = await context.Inquiries.FindAsync(Id);

                if (s != null)
                {
                    context.Inquiries.Remove(s);

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

        public async Task<List<InquiryDTO>> GetAll()
        {
            try
            {
                var inquiry = (await context.Inquiries.OrderByDescending(x=>x.CreateDate).ToListAsync());

                var inquiries = new List<InquiryDTO>();

                foreach (var item in inquiry)
                {
                    var data = new InquiryDTO
                    {
                        Id = item.Id,

                        Name = item.Name,

                        Message = item.Message,

                        PhoneNumber = item.PhoneNumber,

                        CreateDate = item.CreateDate,

                        StatusDescription = GetDescription((InquiryStatus)item.Status),

                    };

                    inquiries.Add(data);
                }
                return inquiries;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<InquiryDTO> GetById(Guid Id)
        {
            try
            {
                var inquiry = await context.Inquiries.FindAsync(Id);

                return new InquiryDTO
                {
                    Id = inquiry.Id,

                    Name = inquiry.Name,

                    Message = inquiry.Message,

                    PhoneNumber = inquiry.PhoneNumber,

                    CreateDate = inquiry.CreateDate,

                    StatusDescription = GetDescription((InquiryStatus)inquiry.Status),

                };


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<InquiryDTO> Update(InquiryDTO inquiryDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Inquiries.FindAsync(inquiryDTO.Id);

                    s.Status = 1;

                    transaction.Commit();
                }

                return inquiryDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        private static object SyncObj = new object();
        static Dictionary<Enum, string> _enumDescriptionCache = new Dictionary<Enum, string>();
        public static string GetDescription(Enum value)
        {
            if (value == null) return string.Empty;

            lock (SyncObj)
            {
                if (!_enumDescriptionCache.ContainsKey(value))
                {
                    var description = (from m in value.GetType().GetMember(value.ToString())
                                       let attr = (DescriptionAttribute)m.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()
                                       select attr == null ? value.ToString() : attr.Description).FirstOrDefault();

                    _enumDescriptionCache.Add(value, description);
                }
            }

            return _enumDescriptionCache[value];
        }
    }
}
