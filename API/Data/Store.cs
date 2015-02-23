namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        public Store()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
