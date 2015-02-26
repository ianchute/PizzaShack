using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository Repository { get; set; }

        public CustomerService(ICustomerRepository repo)
        {
            this.Repository = repo;
        }

        public IEnumerable<Models.CustomerViewModel> List(int page)
        {
            IEnumerable<Models.CustomerViewModel> result = new List<CustomerViewModel>(0);

            try
            { 
                var customerEntities = Repository.List(page);
                var customerModels = new List<CustomerViewModel>(customerEntities.Count());

                foreach(var customerEntity in customerEntities)
                {
                    var customerModel = Mapper.Map<CustomerViewModel>(customerEntity);
                    customerModels.Add(customerModel);
                }

                result = customerModels;
            }
            catch { }

            return result;
        }

        public bool Add(Models.CustomerAddModel model)
        {
            try
            {
                var entity = Mapper.Map<Customer>(model);
                Repository.CreateNewCustomer(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Models.CustomerViewModel Get(Guid id)
        {
            Models.CustomerViewModel result = null;

            try
            {
                var entity = Repository.GetCustomerById(id);
                result = Mapper.Map<CustomerViewModel>(entity);
            }
            catch { }

            return result;
        }

        public bool Edit(Models.CustomerEditModel model)
        {
            try
            {
                var entity = Mapper.Map<Customer>(model);
                Repository.UpdateCustomerDetails(entity);
            }
            catch
            {
                return false;
            }

            return true;
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
    }
}