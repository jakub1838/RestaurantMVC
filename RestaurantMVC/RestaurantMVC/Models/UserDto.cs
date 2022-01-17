using RestaurantMVC.Entities;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public virtual ICollection<OrderDto> Orders { get; set; }
    }
}
