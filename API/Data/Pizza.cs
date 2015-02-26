namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pizza")]
    public partial class Pizza
    {
        public Guid Id { get; set; }

        public DateTime TimeCooked { get; set; }

        public Guid OrderId { get; set; }

        public Guid FlavorId { get; set; }

        public Guid SizeId { get; set; }

        public Guid StoreId { get; set; }

        public virtual Order Order { get; set; }

        public virtual PizzaFlavor PizzaFlavor { get; set; }

        public virtual PizzaSize PizzaSize { get; set; }

        public virtual Store Store { get; set; }
    }
}
