using CarMatt.Data.DTOs.BodyTypeModule;
using CarMatt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarMatt.Data.Services.BodyTypeModule
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly ApplicationDbContext context;
        public BodyTypeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<BodyTypeDTO> Create(BodyTypeDTO bodyTypeDTO)
        {
            try
            {
                var s = new BodyType
                {
                    Id = Guid.NewGuid(),

                    Name = bodyTypeDTO.Name.Substring(0, 1).ToUpper() + bodyTypeDTO.Name.Substring(1).ToLower().Trim(),

                    CreateDate = DateTime.Now,

                    CreatedBy = bodyTypeDTO.CreatedBy

                };

                context.BodyTypes.Add(s);

                await context.SaveChangesAsync();

                return bodyTypeDTO;
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

                var s = await context.BodyTypes.FindAsync(Id);

                if (s != null)
                {
                    context.BodyTypes.Remove(s);

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

        public async Task<List<BodyTypeDTO>> GetAll()
        {
            try
            {
                var makes = (from c in context.BodyTypes

                             join e in context.AppUser on c.CreatedBy equals e.Id

                             select new BodyTypeDTO()
                             {
                                 Id = c.Id,

                                 Name = c.Name,

                                 CreateDate = c.CreateDate,

                                 CreatedBy = c.CreatedBy,

                                 CreatedByName = e.FullName,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await makes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<BodyTypeDTO> GetById(Guid Id)
        {
            try
            {
                var carmake = await context.BodyTypes.FindAsync(Id);

                return new BodyTypeDTO
                {
                    Id = carmake.Id,

                    Name = carmake.Name,

                    CreateDate = carmake.CreateDate,

                    CreatedBy = carmake.CreatedBy
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<BodyTypeDTO> Update(BodyTypeDTO bodyTypeDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.BodyTypes.FindAsync(bodyTypeDTO.Id);

                    s.Name = bodyTypeDTO.Name.Substring(0, 1).ToUpper() + bodyTypeDTO.Name.Substring(1).ToLower().Trim();

                    transaction.Commit();
                }

                await context.SaveChangesAsync();

                return bodyTypeDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
