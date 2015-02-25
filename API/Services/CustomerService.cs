using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository Repository { get; set; }

        public CustomerService(ICustomerRepository repo)
        {
            this.Repository = repo;
        }

        public IEnumerable<Models.CustomerViewModel> List()
        {
            IEnumerable<Models.CustomerViewModel> result = new List<CustomerViewModel>(0);

            try 
            { 
                result = Repository.List(); 
            }
            catch { }

            return result;
        }

        public bool Add(Models.CustomerAddModel customer)
        {
            try
            {
                Repository.CreateNewCustomer(customer);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Models.CustomerViewModel Get(Guid id)
        {
            Models.CustomerViewModel result = new CustomerViewModel();

            try
            {
                result = Repository.GetCustomerById(id);
            }
            catch { }

            return result;
        }

        public bool Delete(Guid id)
        {
            try
            {
                Repository.DeleteCustomerById(id);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Edit(Models.CustomerEditModel customer)
        {
            try
            {
                Repository.UpdateCustomerDetails(customer);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}