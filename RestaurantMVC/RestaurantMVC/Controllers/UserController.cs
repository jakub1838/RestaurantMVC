using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RestaurantMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService accountService;

        public UserController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register([FromForm] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return View(registrationDto);

            try
            {
                accountService.RegisterUser(registrationDto);
            }
            catch
            {
                return View(registrationDto);
            }

            return Ok();
        }

        public IActionResult Login([FromForm] LoginDto loginDto)
        {
            string key = accountService.GenerateJwt(loginDto);

            Response.Cookies.Append("Authorization", key);

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Append("Authorization", "no-token");

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}