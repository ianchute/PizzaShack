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

        public HttpResponseMessage List()
        {
            var customers = Service.List();
            if (customers == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, new List<CustomerViewModel>());
        }

        public HttpResponseMessage Add(CustomerAddModel addModel)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse((HttpStatusCode)422, "Unprocessable Entity");
            var added = Service.Add(addModel);
            if(added)
                return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var result = Service.Get(id);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Delete(Guid id)
        {
            var deleted = Service.Delete(id);
            if (deleted)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Edit(CustomerEditModel editModel)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse((HttpStatusCode)422, "Unprocessable Entity");
            var edited = Service.Edit(editModel);
            if(edited)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
