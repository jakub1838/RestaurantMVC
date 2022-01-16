using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Entities
{
    public enum UserPermissionLevel
    {
        Admin,
        User
    }
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
