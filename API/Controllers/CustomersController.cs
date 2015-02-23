using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using API.Services.Interfaces;

namespace API.Controllers
{
    public class CustomersController : ApiController
    {
        ICustomerService Service { get; set; }

        public CustomersController(ICustomerService service)
        {
            this.Service = service;
        }
    }
}
