using CarMatt.Data.DTOs.CarMakeModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CarMatt.Data.Services.CarMakeModule
{
    public class CarMakeService : ICarMakeService
    {
        private readonly ApplicationDbContext context;
        public CarMakeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CarMakeDTO> Create(CarMakeDTO carMakeDTO)
        {
            try
            {
                var s = new CarMake
                {
                    Id = Guid.NewGuid(),

                    Name = carMakeDTO.Name.Substring(0, 1).ToUpper() + carMakeDTO.Name.Substring(1).ToLower().Trim(),

                    CreateDate = DateTime.Now,

                    CreatedBy = carMakeDTO.CreatedBy

                };

                context.CarMakes.Add(s);

                await context.SaveChangesAsync();

                return carMakeDTO;
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

                var s = await context.CarMakes.FindAsync(Id);

                if (s != null)
                {
                    context.CarMakes.Remove(s);

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

        public async Task<List<CarMakeDTO>> GetAll()
        {
            try
            {
                var makes = (from c in context.CarMakes

                             join e in context.AppUser on c.CreatedBy equals e.Id

                             select new CarMakeDTO()
                             {
                                 Id = c.Id,

                                 Code = c.Code,

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


        public async Task<CarMakeDTO> GetById(Guid Id)
        {
            try
            {
                var carmake = await context.CarMakes.FindAsync(Id);

                return new CarMakeDTO
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

        public async Task<CarMakeDTO> Update(CarMakeDTO carMakeDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.CarMakes.FindAsync(carMakeDTO.Id);

                    s.Name = carMakeDTO.Name.Substring(0, 1).ToUpper() + carMakeDTO.Name.Substring(1).ToLower().Trim();

                    transaction.Commit();
                }

                await context.SaveChangesAsync();

                return carMakeDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
