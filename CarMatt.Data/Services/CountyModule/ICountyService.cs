using CarMatt.Data.DTOs.CountyModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.CountyModule
{
    public interface ICountyService
    {
        Task<List<CountyDTO>> GetAll();
    }
}