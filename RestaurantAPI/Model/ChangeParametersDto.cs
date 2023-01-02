using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Model
{
    public class ChangeParametersDto
    {
        [Required]
        [MaxLength(25)]

        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        public string Description { get; set; }
        [Required]
        public bool HasDelivery { get; set; }
       
    }
}
