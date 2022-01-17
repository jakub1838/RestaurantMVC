using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10, MinimumLength =4)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}     
