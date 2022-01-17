using AutoMapper;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace RestaurantMVC.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        public UserService(RestaurantDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }
        public async Task RegisterUser(Controller controller, RegistrationDto dto)
        {
            using (var dbContextTransaction = await context.Database.BeginTransactionAsync()){
                User user = mapper.Map<User>(dto);

                if (context.Users.Any(x => x.Username == user.Username))
                {
                    controller.ModelState.AddModelError(nameof(RegistrationDto.Username), "This username is taken");
                    throw new ArgumentException();
                }

                if (context.Users.Any(x => x.Email == user.Email))
                {
                    controller.ModelState.AddModelError(nameof(RegistrationDto.Email), "This email is taken");
                    throw new ArgumentException();
                }

                var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
                user.PasswordHash = hashedPassword;

                context.Users.Add(user);

                await context.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
        }
    }
}
