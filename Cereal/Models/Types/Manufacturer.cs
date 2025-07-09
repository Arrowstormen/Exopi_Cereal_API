using System.ComponentModel;

namespace Cereal.Models.Types
{
    public enum Manufacturer
    {
        [Description("American Home Food Products")]
        A = 0, 

        [Description("General Mills")]
        G = 1, 

        [Description("Kelloggs")]
        K = 2, 

        [Description("Nabisco")]
        N = 3, 

        [Description("Post")]
        P = 4, 

        [Description("Quaker Oats")]
        Q = 5, 

        [Description("Ralston Purina")]
        R = 6, 
    }
}
