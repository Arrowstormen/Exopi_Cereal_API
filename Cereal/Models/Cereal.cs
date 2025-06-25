using System.ComponentModel.DataAnnotations;
using Cereal.Models.Types;

namespace Cereal.Models
{
    public class Cereal
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public TempType Type { get; set; }

        //Per serving
        public int Calories { get; set; }

        //Grams
        public int Protein { get; set; }

        //Grams
        public int Fat {  get; set; }

        //Milligrams
        public int Sodium { get; set; }

        //Grams
        public float Fiber { get; set; }

        //Grams
        public float Carbo { get; set; }

        //Grams
        public int Sugars { get; set; }

        //Milligrams
        public int Potass { get; set; }

        //Percentage of 
        public int Vitamins { get; set; }

        //Which display shelf
        public int Shelf { get; set; }

        //Ounces
        public float Weight { get; set; }

        public float Cups { get; set; }

        public float Rating { get; set; }
    }
}
