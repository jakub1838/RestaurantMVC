using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using System.Threading.Tasks;

namespace RestaurantMVC.Services
{
    public interface IUserService
    {
        public Task RegisterUser(Controller controller, RegistrationDto dto);
    }
}
