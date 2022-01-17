using AutoMapper;
using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Middleware;
using System.Security.Claims;
using RestaurantMVC.Exceptions;

namespace RestaurantMVC.Services
{
    public interface IProductService
    {
        public Task<List<ProductDto>> Get();
        public Task<ProductDto> Get(int id);
        public Task Delete(int id, ClaimsPrincipal claims);
        public Task Edit(ProductDto dto, ClaimsPrincipal claims);
        public Task Create(ProductDto dto, ClaimsPrincipal claims);
        public Task<List<ProductDto>> Search(string searchPhrase);
    }
    public class ProductService : IProductService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        public ProductService(RestaurantDbContext context, IMapper mapper, IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task Create(ProductDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                if (!AuthorizeAdmin(claims))
                {
                    throw new ForbidException("");
                }

                Product entity = mapper.Map<Product>(dto);

                User user = accountService.GetUser(claims);

                await context.Products.AddAsync(entity);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task Delete(int id, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Product product = await context.Products.FindAsync(id);

                if (product == null)
                    throw new NotFoundException("");

                if (!AuthorizeAdmin(claims))
                {
                    throw new ForbidException("");
                }

                context.Products.Remove(product);

                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task Edit(ProductDto dto, ClaimsPrincipal claims)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Product entity = mapper.Map<Product>(dto);

                if (!AuthorizeAdmin(claims)) {
                    throw new ForbidException("");
                }

                context.Products.Update(entity);


                await context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task<List<ProductDto>> Get()
        {
            List<Product> entity = await context.Products.ToListAsync();

            List<ProductDto> dto = mapper.Map<List<ProductDto>>(entity);

            return dto;
        }

        public async Task<ProductDto> Get(int id)
        {
            Product entity = await context.Products.FindAsync(id);

            ProductDto dto = mapper.Map<ProductDto>(entity);

            return dto;
        }

        public bool AuthorizeAdmin(ClaimsPrincipal claims)
        {
            User user = accountService.GetUser(claims);

            if (user == null)
                return false;

            return user.RoleId == 1;
        }

        public async Task<List<ProductDto>> Search(string searchPhrase)
        {
            List<Product> entity = await context.Products.ToListAsync();

            List<Product> filteredEntities = entity.Where(x => x.Name.Contains(searchPhrase)).ToList();

            List<ProductDto> dtos = mapper.Map<List<ProductDto>>(entity);

            return dtos;
        }
    }
}
