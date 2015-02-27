using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;

namespace API.Repositories.Interfaces
{
    public interface IDeliveryPersonRepository
    {
        void CreateNewDeliveryPerson(Data.DeliveryPerson deliveryPerson);

        DeliveryPerson GetDeliveryPersonById(Guid mockId);

        void DeleteDeliveryPersonById(Guid mockId);

        void UpdateDeliveryPersonDetails(DeliveryPerson deliveryPerson);

        IEnumerable<DeliveryPerson> List(int p);
    }
}
