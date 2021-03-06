
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Exceptions;
using RestaurantMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace RestaurantMVC.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegistrationDto dto);
        string GenerateJwt(LoginDto dto);
        public User GetUser(ClaimsPrincipal claimPrincipal);
    }
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;
        public AccountService(RestaurantDbContext context, 
            IMapper mapper, 
            IPasswordHasher<User> passwordHasher, 
            AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        public User GetUser(ClaimsPrincipal claimPrincipal)
        {
            var claim = claimPrincipal.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return null;

            var userId = int.Parse(claim.Value);

            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }
        public string GenerateJwt(LoginDto dto)
        {
            var user = context.Users.Include(x => x.Role)
                .FirstOrDefault(u => u.Username == dto.Username);

            if(user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegistrationDto dto)
        {
            User newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email
            };

            if (!context.Users.Any())
                newUser.RoleId = 1;
            else
                newUser.RoleId = 2;

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            context.Users.Add(newUser);
            context.SaveChanges();

        }
        public static bool IsUsernameValid(string username)
        {
            if (username.Length < 3)
                return false;
            if (username.Length > 32)
                return false;
            if (username == "Something")
                return false;

            return true;
        }
    }
}
