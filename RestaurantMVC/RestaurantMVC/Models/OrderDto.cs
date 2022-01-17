using RestaurantMVC.Entities;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public virtual ICollection<OrderProducts> Products { get; set; }
        public bool isDone { get; set; }
    }
}
