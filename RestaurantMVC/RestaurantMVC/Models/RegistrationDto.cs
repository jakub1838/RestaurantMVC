using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RestaurantMVC.Models
{
    public class RegistrationDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username length must be betweet 3-32")]
        [MaxLength(32, ErrorMessage = "Username length must be betweet 3-32")]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password length must be betweet 6-32")]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm password must match!")]
        public string ConfirmPassword { get; set; }

    }
}
