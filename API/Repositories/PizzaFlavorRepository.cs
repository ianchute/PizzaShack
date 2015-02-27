using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class PizzaFlavorRepository : IPizzaFlavorRepository
    {
        private Data.IContext Context { get; set; }

        public PizzaFlavorRepository(Data.IContext context)
        {
            this.Context = context;
        }
        public Data.PizzaFlavor GetFlavorById(Guid id)
        {
            var entity = Context.PizzaFlavors.Find(id);
            return entity;
        }

        public void DeleteFlavorById(Guid id)
        {
            var toDelete = Context.PizzaFlavors.Find(id);
            Context.PizzaFlavors.Remove(toDelete);
            Context.Save();
        }

        public IEnumerable<Data.PizzaFlavor> List()
        {
            var result = Context.PizzaFlavors
                .OrderBy(_ => _.FlavorName)
                .ToList();
            return result;
        }

        public void CreateNewFlavor(Data.PizzaFlavor pizzaFlavor)
        {
            Context.PizzaFlavors.Add(pizzaFlavor);
            Context.Save();
        }
    }
}