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

                    Make = vehicleDTO.Make,

                    AvailabilityStatus = vehicleDTO.AvailabilityStatus,

                    Model = vehicleDTO.Model,

                    Kilometres = vehicleDTO.Kilometres,

                    BodyType = vehicleDTO.BodyType,

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
                var vehicle = await context.Vehicles.ToListAsync();

                var vehicles = new List<VehicleDTO>();

                foreach (var item in vehicle)
                {
                    var data = new VehicleDTO
                    {
                        Id = item.Id,

                        Price = item.Price,

                        Make = item.Make,

                        AvailabilityStatus = item.AvailabilityStatus,

                        Model = item.Model,

                        Kilometres = item.Kilometres,

                        BodyType = item.BodyType,

                        StyleTrim = item.StyleTrim,

                        Engine = item.Engine,

                        Drivetrain = item.Drivetrain,

                        Transmission = item.Transmission,

                        ExteriorColor = item.ExteriorColor,

                        InteriorColor = item.InteriorColor,

                        Passangers = item.Passangers,

                        FuelType = item.FuelType,

                        CityFuelEconomy = item.CityFuelEconomy,

                        HighWayFuelEconomy = item.HighWayFuelEconomy,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,
                    };
                    vehicles.Add(data);
                }
                return vehicles;
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
                var vehicle = await context.Vehicles.FindAsync(Id);

                return new VehicleDTO
                {
                    Id = vehicle.Id,

                    Price = vehicle.Price,

                    Make = vehicle.Make,

                    AvailabilityStatus = vehicle.AvailabilityStatus,

                    Model = vehicle.Model,

                    Kilometres = vehicle.Kilometres,

                    BodyType = vehicle.BodyType,

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

                    CreateDate = vehicle.CreateDate,

                    CreatedBy = vehicle.CreatedBy,
                };

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

                    s.Price = vehicleDTO.Price;

                    s.Make = vehicleDTO.Make;

                    s.AvailabilityStatus = vehicleDTO.AvailabilityStatus;

                    s.Model = vehicleDTO.Model;

                    s.Kilometres = vehicleDTO.Kilometres;

                    s.BodyType = vehicleDTO.BodyType;

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
