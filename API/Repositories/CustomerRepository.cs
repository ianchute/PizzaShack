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
using API.Services;
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

        public IEnumerable<Customer> List(int page)
        {
            var result = Context.Customers
                .OrderBy(_ => _.LastName)
                .ThenBy(_ => _.FirstName)
                .Skip(page * Constants.CUSTOMER_PAGE_SIZE)
                .Take(Constants.CUSTOMER_PAGE_SIZE)
                .ToList();
            return result;
        }

        public void CreateNewCustomer(Customer customer)
        {
            Context.Customers.Add(customer);
            Context.Save();
        }

        public Customer GetCustomerById(Guid id)
        {
            var entity = Context.Customers.Find(id);
            return entity;
        }

        public void UpdateCustomerDetails(Customer customer)
        {
            var currentCustomer = Context.Customers.Find(customer.Id);

            currentCustomer.Address = customer.Address;
            currentCustomer.FirstName = customer.FirstName;
            currentCustomer.LastName = customer.LastName;
            currentCustomer.MobileNumber = customer.MobileNumber;

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