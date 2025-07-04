﻿using System.Globalization;
using System.Reflection;
using System.Text;
using Cereal.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cereal.Data
{
    public class CerealContext : DbContext, ICerealContext
    {
        public DbSet<CerealEntity> Cereals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlite("Data Source=cerealdb.db")
                .UseSeeding((context, _) =>
                {
                    string resourceName = Path.Combine("SeedData", "cereal.csv");
                    using (StreamReader reader = new StreamReader(resourceName, Encoding.UTF8))
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            MissingFieldFound = null,
                            Delimiter = ";",
                            PrepareHeaderForMatch = args => args.Header.ToLower(),
                            ShouldSkipRecord = args =>
                            {
                                var rawRow = args.Row.Parser.RawRow;
                                return rawRow == 2; //Ignore second row of types
                            }
                            
                        }; 
                        CsvReader csvReader = new CsvReader(reader, config);
                        var cereals = csvReader.GetRecords<CerealEntity>().ToArray();
                        foreach (var cereal in cereals) 
                        {
                            var existingCereal = Cereals.FirstOrDefault(c => c.Name == cereal.Name);
                            if (existingCereal == null)
                            {
                                Cereals.Add(cereal);
                            } 
                            else
                            {
                                existingCereal = cereal; 
                            }
                        }
                        SaveChangesAsync();
                    }
                });
        

    }
}
