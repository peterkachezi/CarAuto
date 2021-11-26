using CarMatt.Data.DTOs.FeedBackModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.FeedBackModule
{
    public interface IFeedBackService
    {
        Task<FeedBackDTO> Create(FeedBackDTO feedBackDTO);
        Task<FeedBackDTO> Update(FeedBackDTO feedBackDTO);
        Task<List<FeedBackDTO>> GetAll();
        Task<FeedBackDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}