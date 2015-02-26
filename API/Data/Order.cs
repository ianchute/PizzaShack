namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order : BaseEntity
    {
        public Order()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public DateTime TimeOrdered { get; set; }

        public DateTime TimeDelivered { get; set; }

        public Guid CustomerId { get; set; }

        public Guid DeliveryPersonId { get; set; }

        public Guid? PromoId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DeliveryPerson DeliveryPerson { get; set; }

        public virtual PizzaPromo PizzaPromo { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
