﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Controllers
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
    
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        [Required]
        [MaxLength(25)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}