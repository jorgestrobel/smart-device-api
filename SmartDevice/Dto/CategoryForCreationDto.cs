using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartDevice.Models;

namespace SmartDevice.Dto
{
    public class CategoryForCreationDto
    {
        [Required]
        
        public string Name { get; set; }
        public CategoryCodes Codigo { get; set; }
    }
}
