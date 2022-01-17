using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Models
{
    public class ProductDto
    {
        
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 4)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
