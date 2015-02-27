using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IPizzaFlavorRepository
    {

        Data.PizzaFlavor GetFlavorById(Guid mockId);

        void DeleteFlavorById(Guid mockId);

        IEnumerable<Data.PizzaFlavor> List();

        void CreateNewFlavor(Data.PizzaFlavor pizzaFlavor);
    }
}
