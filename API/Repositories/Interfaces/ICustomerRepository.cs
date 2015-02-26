using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;

namespace API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> List(int page);

        void CreateNewCustomer(Customer customer);

        Customer GetCustomerById(Guid id);

        void UpdateCustomerDetails(Customer customerEditModel);

        void DeleteCustomerById(Guid id);
    }
}