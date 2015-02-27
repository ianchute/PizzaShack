using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Controllers;
using API.Services.Interfaces;

namespace API.Infra
{
    public class ContainerConfiguration
    {
        public static void Configure(StructureMap.ConfigurationExpression _)
        {
            _.Scan((x) => {
                x.Assembly("API");
                x.WithDefaultConventions();
            });
            _.ForConcreteType<CustomersController>().Configure
                .Ctor<ICustomerService>().IsTheDefault();
            _.ForConcreteType<DeliveryPersonsController>().Configure
                .Ctor<IDeliveryPersonService>().IsTheDefault();
            _.ForConcreteType<PizzaFlavorsController>().Configure
                .Ctor<IPizzaFlavorService>().IsTheDefault();
        }
    }
}