namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PizzaFlavor")]
    public partial class PizzaFlavor : BaseEntity
    {
        public PizzaFlavor()
        {
            Pizzas = new HashSet<Pizza>();
        }

        [Required]
        [StringLength(50)]
        public string FlavorName { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
