using RestaurantMVC.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public virtual ICollection<OrderProducts> Products { get; set; }
        public bool isDone { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
