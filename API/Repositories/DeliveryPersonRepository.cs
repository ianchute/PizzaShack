using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Data;
using API.Repositories.Interfaces;
using API.Services;

namespace API.Repositories
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        public IContext Context { get; set; }

        public DeliveryPersonRepository(IContext context)
        {
            this.Context = context;
        }

        public void CreateNewDeliveryPerson(Data.DeliveryPerson deliveryPerson)
        {
            Context.DeliveryPersons.Add(deliveryPerson);
            Context.Save();
        }

        public Data.DeliveryPerson GetDeliveryPersonById(Guid id)
        {
            var entity = Context.DeliveryPersons.Find(id);
            return entity;
        }

        public void DeleteDeliveryPersonById(Guid id)
        {
            var toDelete = Context.DeliveryPersons.Find(id);
            Context.DeliveryPersons.Remove(toDelete);
            Context.Save();
        }

        public void UpdateDeliveryPersonDetails(Data.DeliveryPerson deliveryPerson)
        {
            var current = Context.DeliveryPersons.Find(deliveryPerson.Id);

            current.FirstName = deliveryPerson.FirstName;
            current.LastName = deliveryPerson.LastName;

            Context.Save();
        }

        public IEnumerable<Data.DeliveryPerson> List(int page)
        {
            var result = Context.DeliveryPersons
                .OrderBy(_ => _.LastName)
                .ThenBy(_ => _.FirstName)
                .Skip(page * Constants.CUSTOMER_PAGE_SIZE)
                .Take(Constants.CUSTOMER_PAGE_SIZE)
                .ToList();
            return result;
        }
    }
}