using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Controllers;
using API.Models;
using API.Services.Interfaces;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace API.Tests.Tests.Controllers
{
    [Binding]
    public class CustomersControllerSteps
    {
        private const int LIST_SIZE = 100;

        [BeforeScenario]
        public void Prepare()
        {
            var validGuids = Builder<Guid>.CreateListOfSize(LIST_SIZE)
                .Random<Guid>(LIST_SIZE)
                .Build();
            ScenarioContext.Current.Add("ids", validGuids);
        }

        [Given(@"I have a controller")]
        public void GivenIHaveAController()
        {
            var service = ScenarioContext.Current.Get<ICustomerService>("customerSvc");
            var controller = new CustomersController(service);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            ScenarioContext.Current.Add("ctrl", controller);
        }

        [Given(@"I have a customer add model")]
        public void GivenIHaveACustomerAddModel()
        {
            ScenarioContext.Current.Add("addModel", 
                Builder<CustomerAddModel>.CreateNew().Build());
        }

        [Given(@"the model state is valid")]
        public void GivenTheModelStateIsValid()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            controller.ModelState.Clear();
        }

        [Given(@"the model state is invalid")]
        public void GivenTheModelStateIsInvalid()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            controller.ModelState.AddModelError("", "");
        }

        [When(@"I request for a list")]
        public void WhenIRequestForAList()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            HttpResponseMessage response = controller.List();
            ScenarioContext.Current.Add("response", response);
        }

        [When(@"I add a customer add model")]
        public void WhenIAddACustomerAddModel()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            var addModel = ScenarioContext.Current.Get<CustomerAddModel>("addModel");
            HttpResponseMessage response = controller.Add(addModel);
            ScenarioContext.Current.Add("response", response);
        }

        [When(@"I get customer by id")]
        public void WhenIGetCustomerById()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            var id = ScenarioContext.Current.Get<Guid>("id");
            HttpResponseMessage response = controller.Get(id);
            ScenarioContext.Current.Add("response", response);
        }

        [When(@"I update using the customer edit model")]
        public void WhenIUpdateUsingTheCustomerEditModel()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            var editModel = ScenarioContext.Current.Get<CustomerEditModel>("editModel");
            HttpResponseMessage response = controller.Edit(editModel);
            ScenarioContext.Current.Add("response", response);
        }

        [When(@"I delete customer by id")]
        public void WhenIDeleteCustomerById()
        {
            var controller = ScenarioContext.Current.Get<CustomersController>("ctrl");
            var id = ScenarioContext.Current.Get<Guid>("id");
            HttpResponseMessage response = controller.Delete(id);
            ScenarioContext.Current.Add("response", response);
        }

        [Then(@"the response should be ok")]
        public void ThenTheResponseShouldBeOk()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the response content should be an enumerable customer view model")]
        public void ThenTheResponseContentShouldBeAnEnumerableCustomerViewModel()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            object content;
            response.TryGetContentValue<object>(out content);
            Assert.IsInstanceOfType(typeof(IEnumerable<CustomerViewModel>), content);
        }

        [Then(@"the response content should be a customer view model")]
        public void ThenTheResponseContentShouldBeACustomerViewModel()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            object content;
            response.TryGetContentValue<object>(out content);
            Assert.IsInstanceOfType(typeof(CustomerViewModel), content);
        }

        [Then(@"the response should be not found")]
        public void ThenTheResponseShouldBeNotFound()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Then(@"the response should be no content")]
        public void ThenTheResponseShouldBeNoContent()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Given(@"I have an id")]
        public void GivenIHaveAnId()
        {
            ScenarioContext.Current.Add("id", Guid.NewGuid());
        }

        [Then(@"the response should be unprocessable entity")]
        public void ThenTheResponseShouldBeUnprocessableEntity()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            var responseContent = "";
            response.TryGetContentValue<string>(out responseContent);
            // Status 422 = Unprocessable entity.
            Assert.AreEqual(422, (int)response.StatusCode);
            Assert.AreEqual("Unprocessable Entity", responseContent);
        }

        [Given(@"I have a customer edit model")]
        public void GivenIHaveACustomerEditModel()
        {
            ScenarioContext.Current.Add("editModel",
                Builder<CustomerEditModel>.CreateNew().Build());
        }

    }
}