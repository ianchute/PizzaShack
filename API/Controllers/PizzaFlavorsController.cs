using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using API.Services.Interfaces;
using System.Linq;

namespace API.Controllers
{
    public class PizzaFlavorsController : ApiController
    {
        private IPizzaFlavorService Service { get; set; }

        public PizzaFlavorsController(IPizzaFlavorService Service)
        {
            // TODO: Complete member initialization
            this.Service = Service;
        }

        [HttpGet]
        public HttpResponseMessage List()
        {
            var flavors = Service.List();
            return Request.CreateResponse(HttpStatusCode.OK, flavors);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]PizzaFlavorAddModel addModel)
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