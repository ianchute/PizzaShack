using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using API.Models;
using API.Services.Interfaces;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace API.Tests.Tests.Controllers
{
    [Binding]
    public class CustomersControllerSteps2
    {
        [Given(@"I have a mock customer service that returns data when list is called")]
        public void GivenIHaveACustomerServiceThatReturnsDataWhenListIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<IEnumerable<CustomerViewModel>>(_ => _.List())
                .Returns(Builder<CustomerViewModel>.CreateListOfSize(100).Build());
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns null when list is called")]
        public void GivenIHaveACustomerServiceThatReturnsNullWhenListIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<IEnumerable<CustomerViewModel>>(_ => _.List())
                .Returns<CustomerViewModel>(null);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns true when add is called")]
        public void GivenIHaveACustomerServiceThatReturnsTrueWhenAddIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Add(It.IsAny<CustomerAddModel>()))
                .Returns(true);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns false when add is called")]
        public void GivenIHaveACustomerServiceThatReturnsFalseWhenAddIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Add(It.IsAny<CustomerAddModel>()))
                .Returns(false);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns data when get is called")]
        public void GivenIHaveACustomerServiceThatReturnsDataWhenGetIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<CustomerViewModel>(_ => _.Get(It.IsAny<Guid>()))
                .Returns(Builder<CustomerViewModel>.CreateNew().Build());
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns null when get is called")]
        public void GivenIHaveACustomerServiceThatReturnsNullWhenGetIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<CustomerViewModel>(_ => _.Get(It.IsAny<Guid>()))
                .Returns<CustomerViewModel>(null);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns true when edit is called")]
        public void GivenIHaveACustomerServiceThatReturnsTrueWhenEditIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Edit(It.IsAny<CustomerEditModel>()))
                .Returns(true);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer edit model")]
        public void GivenIHaveACustomerEditModel()
        {
            ScenarioContext.Current.Add("editModel", 
                Builder<CustomerEditModel>.CreateNew().Build());
        }

        [Given(@"I have a mock customer service that returns false when edit is called")]
        public void GivenIHaveACustomerServiceThatReturnsFalseWhenEditIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Edit(It.IsAny<CustomerEditModel>()))
                .Returns(false);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns true when delete is called")]
        public void GivenIHaveACustomerServiceThatReturnsTrueWhenDeleteIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Delete(It.IsAny<Guid>()))
                .Returns(true);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Given(@"I have a mock customer service that returns false when delete is called")]
        public void GivenIHaveACustomerServiceThatReturnsFalseWhenDeleteIsCalled()
        {
            var mockSvc = new Mock<ICustomerService>();
            mockSvc
                .Setup<bool>(_ => _.Delete(It.IsAny<Guid>()))
                .Returns(false);
            ScenarioContext.Current.Add("customerSvc", mockSvc.Object);
        }

        [Then(@"the response should be internal server error")]
        public void ThenTheResponseShouldBeInternalServerError()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Then(@"the response should be created")]
        public void ThenTheResponseShouldBeCreated()
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}