using System.Data;
using System.Linq.Dynamic.Core;
using System.Net;
using Cereal.Data;
using Cereal.Models;
using Microsoft.EntityFrameworkCore;

namespace Cereal.Services
{
    public class CerealService (ICerealContext context) : ICerealService
    {
        public async Task<IEnumerable<CerealEntity>> GetAllCereals()
        {
            return await Task.Run(() =>
            {
                return context.Cereals.ToArray();
            });
        }

        public async Task<CerealEntity> GetCerealById(int id)
        {
            var entity = await context.Cereals.FirstOrDefaultAsync(c  => c.Id == id);

            if (entity == null)
            {
                throw new Exception("No cereal with id " + id + " currently exists.");
            }

            return entity;
        }

        public async Task<IEnumerable<CerealEntity>> GetFilteredCereals_Predicate(string predicate)
        {
            return await Task.Run(() =>
            {
                return context.Cereals.Where(predicate).ToArray();
            });
        }

        public async Task<int?> CreateOrUpdateCereal(CerealEntity cereal)
        {
            if (cereal.Id == null)
            {
                context.Cereals.Add(cereal);
                await context.SaveChangesAsync();
                return cereal.Id;
            }
            else
            {
                var entity = await context.Cereals.FirstOrDefaultAsync(c => c.Id == cereal.Id);

                if (entity == null)
                {
                    throw new Exception("No cereal with id " + cereal.Id + " currently exists. New cereals cannot include an id.");
                }

                context.Cereals.Update(entity).CurrentValues.SetValues(cereal);
                await context.SaveChangesAsync();
                return cereal.Id;
            }
        }

        public async Task<HttpStatusCode> DeleteCerealById(int id)
        {
            var entity = await context.Cereals.FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                throw new Exception("No cereal with id " + id + " currently exists.");
            }

            context.Cereals.Remove(entity);
            await context.SaveChangesAsync();
            return HttpStatusCode.NoContent;

        }

        public async Task<FileStream> GetImageByName(string name)
        {
            return await Task.Run(() =>
            {
                var image = File.OpenRead("C:\\test\\random_image.jpeg");
                return image;
            });
        }
    }
}
