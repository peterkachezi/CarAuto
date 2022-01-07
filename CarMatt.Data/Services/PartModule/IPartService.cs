using CarMatt.Data.DTOs.PartModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.PartModule
{
    public interface IPartService
    {
        PartDTO Create(PartDTO partDTO);
        Task<List<PartImageDTO>> GetPartsAndImages();
        Task<List<PartDTO>> GetAllParts();
        Task<PartDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);

    }
}