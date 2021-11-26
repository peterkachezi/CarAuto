using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.DTOs.VehicleModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.VehicleModule
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext context;
        public VehicleService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<VehicleDTO> Create(VehicleDTO vehicleDTO)
        {
            try
            {
                var s = new Vehicle
                {
                    Id = Guid.NewGuid(),

                    Price = vehicleDTO.Price,

                    MakeId = vehicleDTO.MakeId,

                    AvailabilityStatus = vehicleDTO.AvailabilityStatus,

                    ModelId = vehicleDTO.ModelId,

                    Kilometres = vehicleDTO.Kilometres,

                    BodyTypeId = vehicleDTO.BodyTypeId,

                    StyleTrim = vehicleDTO.StyleTrim,

                    Engine = vehicleDTO.Engine,

                    Drivetrain = vehicleDTO.Drivetrain,

                    Transmission = vehicleDTO.Transmission,

                    ExteriorColor = vehicleDTO.ExteriorColor,

                    InteriorColor = vehicleDTO.InteriorColor,

                    Passangers = vehicleDTO.Passangers,

                    FuelType = vehicleDTO.FuelType,

                    CityFuelEconomy = vehicleDTO.CityFuelEconomy,

                    HighWayFuelEconomy = vehicleDTO.HighWayFuelEconomy,

                    CreateDate = DateTime.Now,

                    CreatedBy = vehicleDTO.CreatedBy,

                };

                context.Vehicles.Add(s);

                await context.SaveChangesAsync();

                return vehicleDTO;
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

                var s = await context.Vehicles.FindAsync(Id);

                if (s != null)
                {
                    context.Vehicles.Remove(s);

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

        public async Task<List<VehicleDTO>> GetAll()
        {
            try
            {
                var makes = (from vehicle in context.Vehicles

                             join user in context.AppUser on vehicle.CreatedBy equals user.Id

                             join make in context.CarMakes on vehicle.MakeId equals make.Id

                             join model in context.CarModels on vehicle.ModelId equals model.Id

                             join bodytype in context.BodyTypes on vehicle.BodyTypeId equals bodytype.Id

                             select new VehicleDTO()
                             {
                                 Id = vehicle.Id,

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

                                 Passangers = vehicle.Passangers,

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


        public async Task<VehicleDTO> GetById(Guid Id)
        {
            try
            {
                var makes = (from vehicle in context.Vehicles

                             join user in context.AppUser on vehicle.CreatedBy equals user.Id

                             join make in context.CarMakes on vehicle.MakeId equals make.Id

                             join model in context.CarModels on vehicle.ModelId equals model.Id

                             join bodytype in context.BodyTypes on vehicle.BodyTypeId equals bodytype.Id

                             select new VehicleDTO()
                             {
                                 Id = vehicle.Id,

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

                                 Passangers = vehicle.Passangers,

                                 FuelType = vehicle.FuelType,

                                 CityFuelEconomy = vehicle.CityFuelEconomy,

                                 HighWayFuelEconomy = vehicle.HighWayFuelEconomy,

                                 YearOfProduction = vehicle.YearOfProduction,

                                 CreateDate = vehicle.CreateDate,

                                 CreatedBy = vehicle.CreatedBy,

                                 CreatedByName = user.FullName,

                             });

                return await makes.Where(x => x.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public VehicleDTO SaveUploads(VehicleDTO vehicleDTO)
        {
            try
            {
                vehicleDTO.Id = Guid.NewGuid();

                var s = new Vehicle
                {
                    Id = vehicleDTO.Id,

                    Price = vehicleDTO.Price,

                    MakeId = vehicleDTO.MakeId,

                    AvailabilityStatus = vehicleDTO.AvailabilityStatus,

                    ModelId = vehicleDTO.ModelId,

                    Kilometres = vehicleDTO.Kilometres,

                    BodyTypeId = vehicleDTO.BodyTypeId,

                    StyleTrim = vehicleDTO.StyleTrim,

                    Engine = vehicleDTO.Engine,

                    Drivetrain = vehicleDTO.Drivetrain,

                    Transmission = vehicleDTO.Transmission,

                    ExteriorColor = vehicleDTO.ExteriorColor,

                    InteriorColor = vehicleDTO.InteriorColor,

                    Passangers = vehicleDTO.Passangers,

                    FuelType = vehicleDTO.FuelType,

                    CityFuelEconomy = vehicleDTO.CityFuelEconomy,

                    HighWayFuelEconomy = vehicleDTO.HighWayFuelEconomy,

                    CreateDate = DateTime.Now,

                    CreatedBy = vehicleDTO.CreatedBy,

                    Quantity = vehicleDTO.Quantity,

                    YearOfProduction = vehicleDTO.YearOfProduction,

                };

                context.Vehicles.Add(s);

                var myimage = vehicleDTO.ImageName.ToList();

                var myimages = new List<ImageDTO>();


                //foreach (var item in vehicleDTO.ImageName)
                //{
                //    var im = item;
                //}




                foreach (var item in vehicleDTO.ImageName)
                {
                    var image = new Image();
                    {
                        image.Id = Guid.NewGuid();

                        image.VehicleId = vehicleDTO.Id;

                        image.ImageName = item;

                        image.CreateDate = DateTime.Now;

                        image.CreatedBy = vehicleDTO.CreatedBy;

                    };
                    context.Images.Add(image);
                }

                context.SaveChanges();

                return vehicleDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<VehicleDTO> Update(VehicleDTO vehicleDTO)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Vehicles.FindAsync(vehicleDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }

                    s.Price = vehicleDTO.Price;

                    s.Quantity = vehicleDTO.Quantity;

                    s.MakeId = vehicleDTO.MakeId;

                    s.AvailabilityStatus = vehicleDTO.AvailabilityStatus;

                    s.ModelId = vehicleDTO.ModelId;

                    s.Kilometres = vehicleDTO.Kilometres;

                    s.BodyTypeId = vehicleDTO.BodyTypeId;

                    s.StyleTrim = vehicleDTO.StyleTrim;

                    s.Engine = vehicleDTO.Engine;

                    s.Drivetrain = vehicleDTO.Drivetrain;

                    s.Transmission = vehicleDTO.Transmission;

                    s.ExteriorColor = vehicleDTO.ExteriorColor;

                    s.InteriorColor = vehicleDTO.InteriorColor;

                    s.Passangers = vehicleDTO.Passangers;

                    s.FuelType = vehicleDTO.FuelType;

                    s.CityFuelEconomy = vehicleDTO.CityFuelEconomy;

                    s.HighWayFuelEconomy = vehicleDTO.HighWayFuelEconomy;

                    s.YearOfProduction = vehicleDTO.YearOfProduction;

                    transaction.Commit();


                }

                await context.SaveChangesAsync();

                return vehicleDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
