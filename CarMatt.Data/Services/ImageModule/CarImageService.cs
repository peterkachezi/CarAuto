using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.ImageModule
{
    public class CarImageService : ICarImageService
    {
        private readonly ApplicationDbContext context;

        public CarImageService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<ImageDTO>> GetByCarId(Guid Id)
        {
            try
            {
                var image = await context.Images.Where(x => x.VehicleId == Id).ToListAsync();

                var images = new List<ImageDTO>();

                foreach (var item in image)
                {
                    var data = new ImageDTO
                    {
                        Id = item.Id,

                        CreateDate = item.CreateDate,

                        ImageName = item.ImageName,

                    };

                    images.Add(data);

                }

                return images;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ImageDTO> GetByCardIdSingle(Guid Id)
        {
            try
            {
                var image = await context.Images.Where(x => x.VehicleId == Id).FirstOrDefaultAsync();

                if (image == null)
                {
                    return null;
                }

                return new ImageDTO
                {
                    Id = image.Id,

                    CreateDate = image.CreateDate,

                    ImageName = image.ImageName,

                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<List<ImageDTO>> GetAll()
        {
            try
            {
                var makes = (from img in context.Images

                             join vehicle in context.Vehicles on img.VehicleId equals vehicle.Id

                             join user in context.AppUser on vehicle.CreatedBy equals user.Id

                             join make in context.CarMakes on vehicle.MakeId equals make.Id

                             join model in context.CarModels on vehicle.ModelId equals model.Id

                             join bodytype in context.BodyTypes on vehicle.BodyTypeId equals bodytype.Id

                             select new ImageDTO()
                             {
                                 Id = vehicle.Id,

                                 VehicleId = img.VehicleId,

                                 ImageName=img.ImageName,

                                 Price = vehicle.Price,

                                 Quantity = vehicle.Quantity,

                                 MakeId = vehicle.MakeId,

                                 MakeName = make.Name,

                                 AvailabilityStatus = vehicle.AvailabilityStatus,

                                 ModelId = vehicle.ModelId,

                                 ModelName = model.Name,

                                 Kilometres = vehicle.Kilometres,

                                 BodyTypeId = vehicle.BodyTypeId,

                                 BodyTypeName = bodytype.Name,

                                 StyleTrim = vehicle.StyleTrim,

                                 Engine = vehicle.Engine,

                                 Drivetrain = vehicle.Drivetrain,

                                 Transmission = vehicle.Transmission,

                                 ExteriorColor = vehicle.ExteriorColor,

                                 InteriorColor = vehicle.InteriorColor,

                                 Doors = vehicle.Doors,

                                 FuelType = vehicle.FuelType,

                                 CityFuelEconomy = vehicle.CityFuelEconomy,

                                 HighWayFuelEconomy = vehicle.HighWayFuelEconomy,

                                 YearOfProduction = vehicle.YearOfProduction,

                                 CreateDate = vehicle.CreateDate,

                                 CreatedBy = vehicle.CreatedBy,

                                 CreatedByName = user.FullName,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await makes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<bool> Delete(Guid Id)
        {
            bool result = false;

            var s = await context.Images.Where(x => x.VehicleId==Id).FirstOrDefaultAsync();

            if (s != null)
            {
                context.Images.Remove(s);

                await context.SaveChangesAsync();

                return true;
            }

            return result;
        }
    }
}
