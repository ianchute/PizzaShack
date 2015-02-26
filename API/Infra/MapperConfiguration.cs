using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Data;
using API.Models;

namespace API.Infra
{
    public class MapperConfiguration
    {
        public static void Configure(AutoMapper.IConfiguration _)
        {
            _.CreateMap<CustomerAddModel, Customer>()
                .ForMember(a => a.Id, x => { x.ResolveUsing(new GuidResolver()); })
                .ForMember(a => a.Orders, x => { x.Ignore(); });
            _.CreateMap<CustomerEditModel, Customer>()
                .ForMember(a => a.Orders, x => { x.Ignore(); });
            _.CreateMap<Customer, CustomerViewModel>();
        }
    }
}