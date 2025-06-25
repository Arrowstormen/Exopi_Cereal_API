using Microsoft.EntityFrameworkCore;
using Cereal.Models;

namespace Cereal.Data
{
    public interface ICerealContext
    {
        public DbSet<CerealEntity> Cereals { get; set; }
    }
}
