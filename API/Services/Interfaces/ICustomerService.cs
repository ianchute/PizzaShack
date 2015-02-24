using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Models.CustomerViewModel> List();

        bool Add(Models.CustomerAddModel customerAddModel);

        bool Delete(Guid guid);

        Models.CustomerViewModel Get(Guid guid);

        bool Edit(Models.CustomerEditModel customerEditModel);
    }
}
