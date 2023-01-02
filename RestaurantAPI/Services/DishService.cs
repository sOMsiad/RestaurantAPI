using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Model;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        public int Create(int restaurantId, CreateDishDto dto);
        public DishDto GetById(int restaurantId, int dishId);
        public List<DishDto> GetAll(int restaurantId);
        public void DeleteAll(int restaurantId);
        public void DeleteById(int restaurantId, int dishId);


    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;
            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();
            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant is null) 
                throw new NotFoundException("Restaurant not found");
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException($"Dish not found in [{restaurant.Id}] restaurant");

            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            

            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishDtos;
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurant = GetRestaurantById( restaurantId);


            if (restaurant.Dishes is null)
                throw new NotFoundException($"Dish not found in [{restaurant.Id}] restaurant");
            
            _context.Dishes.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }
        public void DeleteById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = GetDishById(restaurant.Id, dishId);

            if (dish is null)
                throw new NotFoundException($"Dish not found in [{restaurant.Id}] restaurant");

            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(d => d.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            return restaurant;
        }
        private Dish GetDishById(int restaurantId, int dishId)
        {
            var restaurant = _context
                .Restaurants
                .Include(d => d.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null)
                throw new NotFoundException($"Dish [{dishId}] not found in [{restaurantId}] restaurant");


            return dish;
        }
    }
}
