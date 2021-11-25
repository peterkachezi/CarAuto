using CarMatt.Data.DTOs.CarModelModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.CarModelModule
{
    public interface IModelService
    {
        Task<ModelDTO> Create(ModelDTO modelDTO);
        Task<ModelDTO> Update(ModelDTO modelDTO);
        Task<bool> Delete(Guid Id);
        Task<List<ModelDTO>> GetAll();
        Task<ModelDTO> GetById(Guid Id);
    }
}