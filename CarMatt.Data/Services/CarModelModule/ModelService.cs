using CarMatt.Data.DTOs.CarModelModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CarMatt.Data.Services.CarModelModule
{
    public class ModelService : IModelService
    {
        private readonly ApplicationDbContext context;

        public ModelService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ModelDTO> Create(ModelDTO modelDTO)
        {
            try
            {
                var s = new CarModel
                {
                    Id = Guid.NewGuid(),

                    Name = modelDTO.Name.Substring(0, 1).ToUpper() + modelDTO.Name.Substring(1).ToLower().Trim(),

                    CarMakeId = modelDTO.CarMakeId,

                    CreateDate = DateTime.Now,

                    CreatedBy = modelDTO.CreatedBy

                };

                context.CarModels.Add(s);

                await context.SaveChangesAsync();

                return modelDTO;
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

                var s = await context.CarModels.FindAsync();

                if (s != null)
                {
                    context.CarModels.Remove(s);

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

        public async Task<List<ModelDTO>> GetAll()
        {
            try
            {
                var makes = (from c in context.CarModels

                             join e in context.AppUser on c.CreatedBy equals e.Id

                             join m in context.CarMakes on c.CarMakeId equals m.Id

                             select new ModelDTO()
                             {
                                 Id = c.Id,

                                 Name = c.Name,

                                 CarMakeId = c.CarMakeId,

                                 CreateDate = c.CreateDate,

                                 CreatedBy = c.CreatedBy,

                                 CreatedByName = e.FullName,

                                 CarMakeName = m.Name,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await makes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }


        public async Task<ModelDTO> GetById(Guid Id)
        {
            try
            {
                var carmake = await context.CarModels.FindAsync(Id);

                return new ModelDTO
                {
                    Id = carmake.Id,

                    Name = carmake.Name,

                    CarMakeId = carmake.CarMakeId,

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

        public async Task<ModelDTO> Update(ModelDTO modelDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.CarModels.FindAsync(modelDTO.Id);

                    s.Name = modelDTO.Name.Substring(0, 1).ToUpper() + modelDTO.Name.Substring(1).ToLower().Trim();

                    s.CarMakeId = modelDTO.CarMakeId;

                    transaction.Commit();
                }

                await context.SaveChangesAsync();

                return modelDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
