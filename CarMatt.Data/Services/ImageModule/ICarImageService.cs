using CarMatt.Data.DTOs.ImageModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.ImageModule
{
    public interface ICarImageService
    {
        Task<List<ImageDTO>> GetAll();
        Task<List<ImageDTO>> GetByCarId(Guid Id);
        Task<ImageDTO> GetByCardIdSingle(Guid Id);
    }
}