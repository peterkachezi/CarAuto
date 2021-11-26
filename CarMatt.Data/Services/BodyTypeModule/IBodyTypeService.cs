using CarMatt.Data.DTOs.BodyTypeModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.BodyTypeModule
{
    public interface IBodyTypeService
    {
        Task<List<BodyTypeDTO>> GetAll();
        Task<BodyTypeDTO> GetById(Guid Id);
        Task<BodyTypeDTO> Create(BodyTypeDTO bodyTypeDTO);
        Task<BodyTypeDTO> Update(BodyTypeDTO bodyTypeDTO);
        Task<bool> Delete(Guid Id);
    }
}