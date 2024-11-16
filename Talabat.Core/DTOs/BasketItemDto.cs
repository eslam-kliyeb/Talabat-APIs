using System.ComponentModel.DataAnnotations;

namespace Talabat.Core.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Can not be Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be One Item At Least")]
        public int Quantity { get; set; }
    }
}
