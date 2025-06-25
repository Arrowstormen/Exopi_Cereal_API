using Cereal.Models;
using Microsoft.EntityFrameworkCore;

namespace Cereal.Data
{
    public class CerealContext : DbContext, ICerealContext
    {
        public DbSet<CerealEntity> Cereals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cerealdb.db");
        }

    }
}
