using RestaurantMVC.Services;
using RestaurantMVC.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using RestaurantMVC;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantMVC.Models;
using System.Collections.Generic;

namespace RestaurantMVCTests
{
    public class AccountServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("aaavvvaaavvvaaaaavavavavavavavavavavavav")]
        [InlineData("Something")]
        public void UsernameValidation_ForInvalidData_ReturnsFalse(string username)
        {
            // Act
            bool result = AccountService.IsUsernameValid(username);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Andrzej")]
        [InlineData("Kuba")]
        [InlineData("UwU")]
        public void UsernameValidation_ForInvalidData_ReturnsTrue(string username)
        {
            // Act
            bool result = AccountService.IsUsernameValid(username);

            // Assert
            Assert.True(result);
        }
    }
}

