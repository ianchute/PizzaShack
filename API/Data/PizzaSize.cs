namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PizzaSize")]
    public partial class PizzaSize
    {
        public PizzaSize()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SizeName { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}