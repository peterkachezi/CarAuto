using CarMatt.Data.DTOs.PartModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.PartModule
{
    public class PartService : IPartService
    {
        private readonly ApplicationDbContext context;

        public PartService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public PartDTO Create(PartDTO partDTO)
        {
            try
            {
                partDTO.Id = Guid.NewGuid();

                var s = new Part
                {
                    Id = partDTO.Id,

                    Price = partDTO.Price,

                    Name = partDTO.Name,

                    Quantity = partDTO.Quantity,

                    CreatedBy = partDTO.CreatedBy,

                    CreateDate = DateTime.Now,


                };

                context.Parts.Add(s);

                var myimage = partDTO.ImageName.ToList();

                var myimages = new List<PartDTO>();

                foreach (var item in partDTO.ImageName)
                {
                    var image = new PartImage();
                    {
                        image.Id = Guid.NewGuid();

                        image.PartId = partDTO.Id;

                        image.ImageName = item;

                        image.CreateDate = DateTime.Now;

                        image.CreatedBy = partDTO.CreatedBy;

                    };
                    context.PartImages.Add(image);
                }

                context.SaveChanges();

                return partDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<PartImageDTO>> GetPartsAndImages()
        {
            try
            {
                var makes = (from image in context.PartImages

                             join user in context.AppUser on image.CreatedBy equals user.Id

                             join part in context.Parts on image.PartId equals part.Id

                             select new PartImageDTO()
                             {
                                 Id = part.Id,

                                 Name = part.Name,

                                 Price = part.Price,

                                 Quantity = part.Quantity,

                                 CreateDate = part.CreateDate,

                                 CreatedByName = user.FullName,

                                 ImageName = image.ImageName,

                                 PartId = image.PartId,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await makes;
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

                var s = await context.Parts.FindAsync(Id);

                if (s != null)
                {
                    context.Parts.Remove(s);

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

        public Task<PartDTO> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartDTO>> GetAllParts()
        {
            try
            {
                var makes = (from part in context.Parts

                             join user in context.AppUser on part.CreatedBy equals user.Id

                             select new PartDTO()
                             {
                                 Id = part.Id,

                                 Name = part.Name,

                                 Price = part.Price,

                                 Quantity = part.Quantity,

                                 CreateDate = part.CreateDate,

                                 CreatedByName = user.FullName,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await makes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
