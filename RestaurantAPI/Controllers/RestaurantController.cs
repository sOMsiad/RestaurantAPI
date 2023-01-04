﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Model;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
      

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ChangeParametersDto changeParametersDto, [FromRoute] int id )
        {
              _restaurantService.Update(id,changeParametersDto);
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
          
            return NoContent();
          
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
            var id= _restaurantService.Create(dto);
            return Created($"api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize(Policy = "Atleast20")]
        [Authorize(Policy = "AtleastRestaurantCreated")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantDtos = _restaurantService.GetAll();
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]     
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }
    }
}
