using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.ImageModule
{
    public class CarImageService : ICarImageService
    {
        private readonly ApplicationDbContext context;

        public CarImageService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<ImageDTO>> GetByCarId(Guid Id)
        {
            try
            {
                var image = await context.Images.Where(x => x.VehicleId == Id).ToListAsync();

                var images = new List<ImageDTO>();

                foreach (var item in image)
                {
                    var data = new ImageDTO
                    {
                        Id = item.Id,

                        CreateDate = item.CreateDate,

                        ImageName = item.ImageName,

                    };

                    images.Add(data);

                }

                return images;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ImageDTO> GetByCardIdSingle(Guid Id)
        {
            try
            {
                var image = await context.Images.Where(x => x.VehicleId == Id).FirstOrDefaultAsync();

                if (image == null)
                {
                    return null;
                }

                return new ImageDTO
                {
                    Id = image.Id,

                    CreateDate = image.CreateDate,

                    ImageName = image.ImageName,

                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<ImageDTO>> GetAll()
        {
            try
            {
                var image = await context.Images.ToListAsync();

                var images = new List<ImageDTO>();

                foreach (var item in image)
                {
                    var data = new ImageDTO
                    {
                        Id = item.Id,

                        VehicleId = item.VehicleId,

                        CreateDate = item.CreateDate,

                        ImageName = item.ImageName,

                    };

                    images.Add(data);

                }

                return images;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
