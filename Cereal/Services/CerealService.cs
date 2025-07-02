using System.Data;
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

        public async Task<IEnumerable<CerealEntity>> GetFilteredCereals_SetValues(CerealEntity filter)
        {
            return await Task.Run(() =>
             {
                var query = context.Cereals;

                 foreach (var prop in filter.GetType().GetProperties())
                 {
                     query = (DbSet<CerealEntity>)query.Where(c => c.GetType().GetProperty(prop.Name.ToString()).GetValue(c, null) == prop.GetValue(filter, null));
                 }

                 return query.ToArray();
             });
        }

        public async Task<(HttpStatusCode,int?)> CreateOrUpdateCereal(CerealEntity cereal)
        {
            if (cereal.Id == null)
            {
                context.Cereals.Add(cereal);
                await context.SaveChangesAsync();
                return (HttpStatusCode.Created,cereal.Id);
            }
            else
            {
                var entity = await context.Cereals.FirstOrDefaultAsync(c => c.Id == cereal.Id);

                if (entity == null)
                {
                    throw new Exception("No cereal with id " + cereal.Id + " currently exists. New cereals cannot include an id.");
                }

                entity = cereal;
                await context.SaveChangesAsync();
                return (HttpStatusCode.OK,cereal.Id);
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
    }
}
