using System;
using System.Data.Entity;
using System.Linq;
using API.Data;
using API.Infra;
using API.Models;
using API.Repositories;
using AutoMapper;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace API.Tests.Tests
{
    [Binding]
    public class CustomerRepositorySteps
    {
        [Given(@"I have a mock customer database context")]
        public void GivenIHaveAMockCustomerDatabaseContext()
        {
            var mockDbSet = new MockDbSet<Customer>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            ScenarioContext.Current.Add("customerContext", mockContext.Object);
        }

        [Given(@"I have a customer repository")]
        public void GivenIHaveACustomerRepository()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var repository = new CustomerRepository(context);
            ScenarioContext.Current.Add("customerRepo", repository);
        }

        [Given(@"A customer to delete exists in the mock database set")]
        public void GivenACustomerToDeleteExistsInTheMockDatabaseSet()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var customerToDelete = Builder<Customer>.CreateNew().Build();
            context.Customers.Add(customerToDelete);
            ScenarioContext.Current.Add("customerToDelete", customerToDelete);
        }

        [Given(@"A customer to get exists in the mock database set")]
        public void GivenACustomerToGetExistsInTheMockDatabaseSet()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var customerToGet = Builder<Customer>.CreateNew().Build();
            context.Customers.Add(customerToGet);
            ScenarioContext.Current.Add("id", customerToGet.Id);
        }


        [Given(@"I have an id of the customer to delete")]
        public void GivenIHaveAnIdOfTheCustomerToDelete()
        {
            var customer = ScenarioContext.Current.Get<Customer>("customerToDelete");
            var idOfCustomerToDelete = customer.Id;
            ScenarioContext.Current.Add("id", idOfCustomerToDelete);
        }

        [When(@"I get the customer list from the repository")]
        public void WhenIGetTheCustomerListFromTheRepository()
        {
            var repo = ScenarioContext.Current.Get<CustomerRepository>("customerRepo");
            var retVal = repo.List();
            ScenarioContext.Current.Add("retVal", retVal);
        }

        [When(@"I ask the repository to add the customer add model")]
        public void WhenIAskTheRepositoryToAddTheCustomerAddModel()
        {
            var repo = ScenarioContext.Current.Get<CustomerRepository>("customerRepo");
            var addModel = ScenarioContext.Current.Get<CustomerAddModel>("addModel");
            repo.CreateNewCustomer(addModel);
        }

        [When(@"I get customer by id from repository")]
        public void WhenIGetCustomerByIdFromRepository()
        {
            var repo = ScenarioContext.Current.Get<CustomerRepository>("customerRepo");
            var id = ScenarioContext.Current.Get<Guid>("id");
            var retVal = repo.GetCustomerById(id);
            ScenarioContext.Current.Add("retVal", retVal);
        }

        [When(@"I update using the customer edit model using the repository")]
        public void WhenIUpdateUsingTheCustomerEditModelUsingTheRepository()
        {
            var repo = ScenarioContext.Current.Get<CustomerRepository>("customerRepo");
            var editModel = ScenarioContext.Current.Get<CustomerEditModel>("editModel");
            repo.UpdateCustomerDetails(editModel);
        }

        [When(@"I delete customer by id using the repository")]
        public void WhenIDeleteCustomerByIdUsingTheRepository()
        {
            var repo = ScenarioContext.Current.Get<CustomerRepository>("customerRepo");
            var id = ScenarioContext.Current.Get<Guid>("id");
            repo.DeleteCustomerById(id);
        }

        [Then(@"the mock customer database context must contain the added customer")]
        public void ThenTheMockCustomerDatabaseContextMustContainTheAddedCustomer()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var addModel = ScenarioContext.Current.Get<CustomerAddModel>("addModel");
            var contains = context.Customers.Count() != 0;
            Assert.IsTrue(contains);
        }

        [Then(@"the mock customer database context must contain the edited customer")]
        public void ThenTheMockCustomerDatabaseContextMustContainTheEditedCustomer()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var editModel = ScenarioContext.Current.Get<CustomerEditModel>("editModel");
            var contains = context.Customers.Any(customer => customer.FirstName == editModel.FirstName);
            Assert.IsTrue(contains);
        }

        [Then(@"the mock customer database context must not contain the deleted customer")]
        public void ThenTheMockCustomerDatabaseContextMustNotContainTheDeletedCustomer()
        {
            var context = ScenarioContext.Current.Get<IContext>("customerContext");
            var deletedModel = ScenarioContext.Current.Get<Customer>("customerToDelete");
            var contains = context.Customers.Any(customer => customer.FirstName == deletedModel.FirstName);
            Assert.IsFalse(contains);
        }

        [Given(@"The mapping service has been configured")]
        public void GivenTheMappingServiceHasBeenConfigured()
        {
            Mapper.Reset();
            Mapper.Initialize(MapperConfiguration.Configure);
        }

    }
}