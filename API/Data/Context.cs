namespace API.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext, IContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<Pizza> Pizzas { get; set; }
        public virtual IDbSet<PizzaFlavor> PizzaFlavors { get; set; }
        public virtual IDbSet<PizzaPromo> PizzaPromoes { get; set; }
        public virtual IDbSet<PizzaSize> PizzaSizes { get; set; }
        public virtual IDbSet<Store> Stores { get; set; }

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
    }
}
