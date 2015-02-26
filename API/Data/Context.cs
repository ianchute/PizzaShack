namespace API.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Context : DbContext, IContext
    {
        public Context()
            : base("name=Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeliveryPerson>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<DeliveryPerson>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<DeliveryPerson>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.DeliveryPerson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Pizzas)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PizzaFlavor>()
                .Property(e => e.FlavorName)
                .IsUnicode(false);

            modelBuilder.Entity<PizzaFlavor>()
                .HasMany(e => e.Pizzas)
                .WithRequired(e => e.PizzaFlavor)
                .HasForeignKey(e => e.FlavorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PizzaPromo>()
                .Property(e => e.PromoName)
                .IsUnicode(false);

            modelBuilder.Entity<PizzaPromo>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.PizzaPromo)
                .HasForeignKey(e => e.PromoId);

            modelBuilder.Entity<PizzaSize>()
                .Property(e => e.SizeName)
                .IsUnicode(false);

            modelBuilder.Entity<PizzaSize>()
                .HasMany(e => e.Pizzas)
                .WithRequired(e => e.PizzaSize)
                .HasForeignKey(e => e.SizeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.StoreName)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Pizzas)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public IDbSet<Customer> Customers
        {
            get { return this.Set<Customer>(); }
            set { }
        }

        public IDbSet<DeliveryPerson> DeliveryPersons
        {
            get { return this.Set<DeliveryPerson>(); }
            set { }
        }

        public IDbSet<Order> Orders
        {
            get { return this.Set<Order>(); }
            set { }
        }

        public IDbSet<PizzaFlavor> PizzaFlavors
        {
            get { return this.Set<PizzaFlavor>(); }
            set { }
        }

        public IDbSet<PizzaPromo> PizzaPromoes
        {
            get { return this.Set<PizzaPromo>(); }
            set { }
        }

        public IDbSet<Pizza> Pizzas
        {
            get { return this.Set<Pizza>(); }
            set { }
        }

        public IDbSet<PizzaSize> PizzaSizes
        {
            get { return this.Set<PizzaSize>(); }
            set { }
        }

        public IDbSet<Store> Stores
        {
            get { return this.Set<Store>(); }
            set { }
        }
    }
}