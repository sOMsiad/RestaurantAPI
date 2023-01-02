using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Maager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "unhealthy",
                    ContactEmail = "cos@kfc.pl",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "First unhealthy dish",
                            Price = 5.4M,
                        },
                        new Dish()
                        {
                            Name = "Second unhealthy dish",
                            Price = 50.4M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "dluga 2",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "MC",
                    Category = "Fast Food",
                    Description = "the most unhealthy ",
                    ContactEmail = "cos@mc.pl",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "First the most unhealthy dish",
                            Price = 5.4M,
                        },
                        new Dish()
                        {
                            Name = "Second the most unhealthy dish",
                            Price = 50.4M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Sosnowiec",
                        Street = "Madzi 22",
                        PostalCode = "40-001"
                    }

                }
            };
            return restaurants;
        }
    }
}
