using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public User User { get; set; }

        public virtual ICollection<OrderProducts> Products { get; set; }

        public bool isDone { get; set; }
    }
}
