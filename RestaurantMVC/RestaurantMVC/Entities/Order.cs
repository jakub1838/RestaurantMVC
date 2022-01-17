using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Entities
{
    public class Order
    {
        public Order()
        {
            Products = new List<OrderProducts>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<OrderProducts> Products { get; set; }

        public bool isDone { get; set; }
    }
}
