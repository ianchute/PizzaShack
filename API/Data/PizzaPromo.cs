namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PizzaPromo")]
    public partial class PizzaPromo
    {
        public PizzaPromo()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PromoName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
