using System.ComponentModel.DataAnnotations;
using Cereal.Models.Types;
using CsvHelper.Configuration.Attributes;

namespace Cereal.Models
{
    public class CerealEntity
    {

        [Ignore]
        public int? Id { get; set; }

        [Index(0)]
        [Required]
        public string Name { get; set; }

        [Name("mfr")]
        [Index(1)]
        public Manufacturer Manufacturer { get; set; }

        [Index(2)]
        public TempType Type { get; set; }

        [Index(3)]
        //Per serving
        public int Calories { get; set; }

        [Index(4)]
        //Grams
        public int Protein { get; set; }

        [Index(5)]
        //Grams
        public int Fat {  get; set; }

        [Index(6)]
        //Milligrams
        public int Sodium { get; set; }

        [Index(7)]
        //Grams
        public float Fiber { get; set; }

        [Index(8)]
        //Grams
        public float     Carbo { get; set; }

        [Index(9)]
        //Grams
        public int Sugars { get; set; }

        [Index(10)]
        //Milligrams
        public int Potass { get; set; }

        [Index(11)]
        //Percentage of 
        public int Vitamins { get; set; }

        [Index(12)]
        //Which display shelf
        public int Shelf { get; set; }

        [Index(13)]
        //Ounces
        public float Weight { get; set; }

        [Index(14)]
        public float Cups { get; set; }

        [Index(15)]
        public float Rating { get; set; }
    }
}
