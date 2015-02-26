using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace API.Data
{
    public interface IContext
    {
        IDbSet<Customer> Customers { get; set; }
        IDbSet<DeliveryPerson> DeliveryPersons { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<PizzaFlavor> PizzaFlavors { get; set; }
        IDbSet<PizzaPromo> PizzaPromoes { get; set; }
        IDbSet<Pizza> Pizzas { get; set; }
        IDbSet<PizzaSize> PizzaSizes { get; set; }
        IDbSet<Store> Stores { get; set; }
        void Save();
    }
}