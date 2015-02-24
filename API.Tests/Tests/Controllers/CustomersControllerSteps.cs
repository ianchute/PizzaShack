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

        [Given(@"I have a valid id")]
        public void GivenIHaveAValidId()
        {
            var validGuids = ScenarioContext.Current.Get<IList<Guid>>("ids");
            var validId = Pick<Guid>.RandomItemFrom(validGuids);
            ScenarioContext.Current.Add("id", validId);
        }

        [Given(@"I have an invalid id")]
        public void GivenIHaveAnInvalidId()
        {
            var validGuids = ScenarioContext.Current.Get<IList<Guid>>("ids");
            var invalidId = Guid.Empty;
            do
            {
                invalidId = Services.Generator.Guid();
            } while (validGuids.Contains(invalidId));
            ScenarioContext.Current.Add("id", invalidId);
        }

        [Given(@"I have a customer edit model with a valid id")]
        public void GivenIHaveACustomerEditModelWithAValidId()
        {
            var validGuids = ScenarioContext.Current.Get<IList<Guid>>("ids");
            var validId = Pick<Guid>.RandomItemFrom(validGuids);
            ScenarioContext.Current.Add("editModel",
                Builder<CustomerEditModel>.CreateNew().And(_ => { _.Id = validId; }).Build());
        }

        [Given(@"I have a customer edit model with an invalid id")]
        public void GivenIHaveACustomerEditModelWithAnInvalidId()
        {
            ScenarioContext.Current.Add("editModel",
                Builder<CustomerEditModel>.CreateNew().And(_ => { _.Id = GenerateInvalidId(); }).Build());
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

        //private ICustomerService CreateMockCustomerService()
        //{
        //    var validGuids = ScenarioContext.Current.Get<IList<Guid>>("ids");
        //    var serviceMock = new Mock<ICustomerService>();

        //    // List
        //    serviceMock.Setup<IEnumerable<CustomerViewModel>>(_ =>
        //        _.List())
        //        .Returns(Builder<CustomerViewModel>.CreateListOfSize(LIST_SIZE).Build());
        //    // Add
        //    serviceMock.Setup(_ =>
        //        _.Add(It.IsAny<CustomerAddModel>()));
        //    // Get
        //    serviceMock.Setup<CustomerViewModel>(_ =>
        //        _.Get(It.IsIn<Guid>(validGuids)))
        //        .Returns(Builder<CustomerViewModel>.CreateNew().Build());
        //    serviceMock.Setup<CustomerViewModel>(_ =>
        //        _.Get(It.IsNotIn<Guid>(validGuids)))
        //        .Returns<CustomerViewModel>(null);
        //    // Edit
        //    serviceMock.Setup<bool>(_ =>
        //        _.Edit(It.Is<CustomerEditModel>(x => validGuids.Contains(x.Id))))
        //        .Returns(true);
        //    serviceMock.Setup<bool>(_ =>
        //        _.Edit(It.Is<CustomerEditModel>(x => !validGuids.Contains(x.Id))))
        //        .Returns(false);
        //    // Delete
        //    serviceMock.Setup<bool>(_ =>
        //        _.Delete(It.IsIn<Guid>(validGuids)))
        //        .Returns(true);
        //    serviceMock.Setup<bool>(_ =>
        //        _.Delete(It.IsNotIn<Guid>(validGuids)))
        //        .Returns(false);
        //    return serviceMock.Object;
        //}

        private Guid GenerateInvalidId()
        {
            var validGuids = ScenarioContext.Current.Get<IList<Guid>>("ids");
            var invalidId = Guid.Empty;
            do
            {
                invalidId = Services.Generator.Guid();
            } while (validGuids.Contains(invalidId));
            return invalidId;
        }
    }
}