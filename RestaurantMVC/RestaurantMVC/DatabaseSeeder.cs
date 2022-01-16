using RestaurantMVC.Data;
using RestaurantMVC.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantMVC
{
    public class DatabaseSeeder
    {
        private readonly RestaurantDbContext dbContext;
        public DatabaseSeeder(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            dbContext.Database.EnsureCreated();
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles()); 
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin",
                },
                new Role()
                {
                    Name = "User"
                }
            };
            return roles;
        }
    }
}
