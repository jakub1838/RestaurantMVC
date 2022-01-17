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
    public class ProductServiceTests
    {
        public static IEnumerable<object[]> TestSearchPersonItemsDataOk =>
        new List<object[]>
        {
            new object[] { new ProductDto { Name = "Ogórki", Price = -22 } },
            new object[] { new ProductDto { Name = "Kiełbasa", Price = 0 } },
            new object[] { new ProductDto { Name = "", Price = -22 } }
        };

        [Theory]
        [MemberData(nameof(TestSearchPersonItemsDataOk))]
        public void ProductValidation_ForInvalidData_ReturnFalse(ProductDto productDto)
        {
            // Arrage


            // Act
            bool result = ProductService.IsProductValid(productDto);

            // Assert
            Assert.False(result);
        }


        public static IEnumerable<object[]> TestSearchPersonItemsDataNotOk =>
        new List<object[]>
        {
            new object[] { new ProductDto { Name = "Ogórki", Price = 1055 } },
            new object[] { new ProductDto { Name = "Kiełbasa", Price = 32 } },
            new object[] { new ProductDto { Name = "Parówki", Price = 22 } }
        };

        [Theory]
        [MemberData(nameof(TestSearchPersonItemsDataNotOk))]
        public void ProductValidation_ForInvalidData_ReturnTrue(ProductDto productDto)
        {
            // Arrage

            // Act
            bool result = ProductService.IsProductValid(productDto);

            // Assert
            Assert.True(result);
        }
    }
}
