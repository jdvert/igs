using System.ComponentModel.DataAnnotations;

namespace IgsMarket.Api.Http
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
