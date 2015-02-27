using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PizzaFlavorAddModel
    {
        [Required]
        [StringLength(50)]
        public string FlavorName { get; set; }
    }
}