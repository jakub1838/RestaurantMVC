using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
