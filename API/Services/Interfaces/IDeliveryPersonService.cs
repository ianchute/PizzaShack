using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Services.Interfaces
{
    public interface IDeliveryPersonService
    {
        IEnumerable<DeliveryPersonViewModel> List(int id);

        bool Add(Models.DeliveryPersonAddModel addModel);

        DeliveryPersonViewModel Get(Guid id);

        bool Edit(Models.DeliveryPersonEditModel editModel);

        bool Delete(Guid id);
    }
}
