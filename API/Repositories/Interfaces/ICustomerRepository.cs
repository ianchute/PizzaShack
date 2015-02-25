using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Models.CustomerViewModel> List();

        void CreateNewCustomer(Models.CustomerAddModel customerAddModel);

        Models.CustomerViewModel GetCustomerById(Guid guid);

        void UpdateCustomerDetails(Models.CustomerEditModel customerEditModel);

        void DeleteCustomerById(Guid guid);
    }
}