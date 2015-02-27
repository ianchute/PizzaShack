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
    public class DeliveryPersonsController : ApiController
    {
        IDeliveryPersonService Service { get; set; }

        public DeliveryPersonsController(IDeliveryPersonService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public HttpResponseMessage List([FromBody]int id)
        {
            var deliveryPersons = Service.List(id);
            return Request.CreateResponse(HttpStatusCode.OK, deliveryPersons);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]DeliveryPersonAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values
                    .SelectMany(_ => _.Errors.Select(x => x.ErrorMessage));
                return Request.CreateResponse((HttpStatusCode)422, validationErrors);
            }
            var added = Service.Add(addModel);
            if(added)
                return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid id)
        {
            var result = Service.Get(id);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPut]
        public HttpResponseMessage Edit([FromBody]DeliveryPersonEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values
                    .SelectMany(_ => _.Errors.Select(x => x.ErrorMessage));
                return Request.CreateResponse((HttpStatusCode)422, validationErrors);
            }
            var edited = Service.Edit(editModel);
            if (edited)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            var deleted = Service.Delete(id);
            if (deleted)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}