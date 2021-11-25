using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.DTOs.VehicleModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.VehicleModule
{
    public interface IVehicleService
    {
        Task<VehicleDTO> Create(VehicleDTO vehicleDTO);
        Task<VehicleDTO> Update(VehicleDTO vehicleDTO);
        Task<bool> Delete(Guid Id);
        Task<List<VehicleDTO>> GetAll();
        Task<VehicleDTO> GetById(Guid Id);
        VehicleDTO SaveUploads(VehicleDTO vehicleDTO);
    }
}