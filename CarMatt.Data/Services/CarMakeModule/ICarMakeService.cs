using CarMatt.Data.DTOs.CarMakeModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.CarMakeModule
{
    public interface ICarMakeService
    {
        Task<CarMakeDTO> Create(CarMakeDTO carMakeDTO);
        Task<CarMakeDTO> Update(CarMakeDTO carMakeDTO);
        Task<bool> Delete(Guid Id);
        Task<List<CarMakeDTO>> GetAll();
        Task<CarMakeDTO> GetById(Guid Id);
    }
}