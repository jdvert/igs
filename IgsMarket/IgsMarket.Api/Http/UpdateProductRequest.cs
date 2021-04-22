using System.ComponentModel.DataAnnotations;

namespace IgsMarket.Api.Http
{
    public class UpdateProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double? Price { get; set; }
    }
}
