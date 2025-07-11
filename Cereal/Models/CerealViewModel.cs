namespace Cereal.Models
{
    public class CerealViewModel
    {
        public List<CerealEntity?> Cereals { get; set; }
        public List<string> ImagePaths { get; set; }

        public string Predicate { get; set; }
    }
}
