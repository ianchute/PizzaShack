using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Services.Interfaces
{
    public interface IPizzaFlavorService
    {
        bool Add(Models.PizzaFlavorAddModel addModel);

        PizzaFlavorViewModel Get(Guid id);

        bool Delete(Guid id);

        IEnumerable<PizzaFlavorViewModel> List();
    }
}
