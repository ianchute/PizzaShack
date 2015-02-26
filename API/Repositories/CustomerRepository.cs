using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public IContext Context { get; set; }

        public CustomerRepository(IContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Models.CustomerViewModel> List()
        {
            var result = new List<CustomerViewModel>(Context.Customers.Count());
            foreach (var customer in Context.Customers)
                result.Add(Mapper.Map<CustomerViewModel>(customer));
            return result;
        }

        public void CreateNewCustomer(Models.CustomerAddModel customerAddModel)
        {
            var entity = Mapper.Map<Customer>(customerAddModel);
            Context.Customers.Add(entity);
            Context.Save();
        }

        public Models.CustomerViewModel GetCustomerById(Guid id)
        {
            var entity = Context.Customers.Find(id);
            return Mapper.Map<CustomerViewModel>(entity);
        }

        public void UpdateCustomerDetails(Models.CustomerEditModel customerEditModel)
        {
            var entity = Mapper.Map<Customer>(customerEditModel);
            Context.Customers.Add(entity);
            Context.Save();
        }

        public void DeleteCustomerById(Guid id)
        {
            var customerToDelete = Context.Customers.Find(id);
            Context.Customers.Remove(customerToDelete);
            Context.Save();
        }
    }
}