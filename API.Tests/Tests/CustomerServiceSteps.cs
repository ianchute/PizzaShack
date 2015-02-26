using System;
using System.Collections.Generic;
using API.Models;
using API.Repositories.Interfaces;
using API.Services;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Linq;

namespace API.Tests.Tests
{
    [Binding]
    public class CustomerServiceSteps
    {
        [Given(@"I have a mock customer repository")]
        public void GivenIHaveAMockCustomerRepository()
        {
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup<IEnumerable<CustomerViewModel>>(_ => _.List())
                .Returns(Builder<CustomerViewModel>.CreateListOfSize(100).Build());
            mockRepository.Setup(_ => _.CreateNewCustomer(It.IsAny<CustomerAddModel>()));
            mockRepository.Setup<CustomerViewModel>(_ => _.GetCustomerById(It.IsAny<Guid>()))
                .Returns(Builder<CustomerViewModel>.CreateNew().Build());
            mockRepository.Setup(_ => _.UpdateCustomerDetails(It.IsAny<CustomerEditModel>()));
            mockRepository.Setup(_ => _.DeleteCustomerById(It.IsAny<Guid>()));

            ScenarioContext.Current.Add("customerRepo", mockRepository.Object);
        }

        [Given(@"I have a mock customer repository that throws exceptions")]
        public void GivenIHaveAMockCustomerRepositoryThatThrowsExceptions()
        {
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup<IEnumerable<CustomerViewModel>>(_ => _.List())
                .Throws<Exception>();
            mockRepository.Setup(_ => _.CreateNewCustomer(It.IsAny<CustomerAddModel>()))
                .Throws<Exception>();
            mockRepository.Setup<CustomerViewModel>(_ => _.GetCustomerById(It.IsAny<Guid>()))
                .Throws<Exception>();
            mockRepository.Setup(_ => _.UpdateCustomerDetails(It.IsAny<CustomerEditModel>()))
                .Throws<Exception>();
            mockRepository.Setup(_ => _.DeleteCustomerById(It.IsAny<Guid>()))
                .Throws<Exception>();

            ScenarioContext.Current.Add("customerRepo", mockRepository.Object);
        }

        [Given(@"I have a customer service")]
        public void GivenIHaveACustomerService()
        {
            var repository = ScenarioContext.Current.Get<ICustomerRepository>("customerRepo");
            var service = new CustomerService(repository);
            ScenarioContext.Current.Add("customerSvc", service);
        }

        [When(@"I get the customer list from the service")]
        public void WhenIGetTheCustomerListFromTheService()
        {
            var service = ScenarioContext.Current.Get<CustomerService>("customerSvc");
            var returnValue = service.List();
            ScenarioContext.Current.Add("retVal", returnValue); 
        }

        [When(@"I ask the service to add the customer add model")]
        public void WhenIAskTheServiceToAddTheCustomerAddModel()
        {
            var service = ScenarioContext.Current.Get<CustomerService>("customerSvc");
            var addModel = ScenarioContext.Current.Get<CustomerAddModel>("addModel");
            var returnValue = service.Add(addModel);
            ScenarioContext.Current.Add("retVal", returnValue); 
        }

        [When(@"I get customer by id from service")]
        public void WhenIGetCustomerByIdFromService()
        {
            var service = ScenarioContext.Current.Get<CustomerService>("customerSvc");
            var id = ScenarioContext.Current.Get<Guid>("id");
            var returnValue = service.Get(id);
            ScenarioContext.Current.Add("retVal", returnValue); 
        }

        [When(@"I update using the customer edit model using the service")]
        public void WhenIUpdateUsingTheCustomerEditModelUsingTheService()
        {
            var service = ScenarioContext.Current.Get<CustomerService>("customerSvc");
            var editModel = ScenarioContext.Current.Get<CustomerEditModel>("editModel");
            var returnValue = service.Edit(editModel);
            ScenarioContext.Current.Add("retVal", returnValue); 
        }

        [When(@"I delete customer by id using the service")]
        public void WhenIDeleteCustomerByIdUsingTheService()
        {
            var service = ScenarioContext.Current.Get<CustomerService>("customerSvc");
            var id = ScenarioContext.Current.Get<Guid>("id");
            var returnValue = service.Delete(id);
            ScenarioContext.Current.Add("retVal", returnValue); 
        }

        [Then(@"the return value should be a non-empty enumerable of customer view models")]
        public void ThenTheReturnValueShouldBeANon_EmptyEnumerableOfCustomerViewModels()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.IsInstanceOfType(typeof(IEnumerable<CustomerViewModel>), retVal);
            Assert.AreNotEqual(0, (retVal as IEnumerable<CustomerViewModel>).Count());
        }

        [Then(@"the return value should be true")]
        public void ThenTheReturnValueShouldBeTrue()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.AreEqual(true, retVal);
        }

        [Then(@"the return value should be false")]
        public void ThenTheReturnValueShouldBeFalse()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.AreEqual(false, retVal);
        }

        [Then(@"the return value should be a customer view model")]
        public void ThenTheReturnValueShouldBeACustomerViewModel()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.IsInstanceOfType(typeof(CustomerViewModel), retVal);
        }

        [Then(@"the return value should be an empty enumerable of customer view models")]
        public void ThenTheReturnValueShouldBeAnEmptyEnumerableOfCustomerViewModels()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.IsInstanceOfType(typeof(IEnumerable<CustomerViewModel>), retVal);
            Assert.AreEqual(0, (retVal as IEnumerable<CustomerViewModel>).Count());
        }

        [Then(@"the return value should be an empty customer view model")]
        public void ThenTheReturnValueShouldBeAnEmptyCustomerViewModel()
        {
            var retVal = ScenarioContext.Current.Get<object>("retVal");
            Assert.IsInstanceOfType(typeof(CustomerViewModel), retVal);
            Assert.IsNotNull(retVal);
        }

    }
}