using System.Net;
using Cereal.Models;

namespace Cereal.Services
{
    public interface ICerealService
    {
        public Task<IEnumerable<CerealEntity>> GetAllCereals();

        public Task<CerealEntity> GetCerealById(int id);

        public Task<IEnumerable<CerealEntity>> GetFilteredCereals_SetValues(CerealEntity filter);

        public Task<(HttpStatusCode,int?)> CreateOrUpdateCereal(CerealEntity cereal);

        public Task<HttpStatusCode> DeleteCerealById(int id);
    }
}
