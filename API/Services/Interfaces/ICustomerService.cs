using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICustomerService
    {
        void Add(Models.CustomerAddModel model);

        IEnumerable<Models.CustomerViewModel> List();

        void Update(Models.CustomerEditModel model);
    }
}
