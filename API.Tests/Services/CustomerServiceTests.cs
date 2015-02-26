using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Services;
using NUnit.Framework;
using API.Repositories.Interfaces;
using Moq;
using API.Models;
using FizzWare.NBuilder;
using System.Web.Http;
using System.Net.Http;
using API.Data;
using AutoMapper;
using API.Infra;

namespace API.Services.Tests
{
    [TestFixture()]
    public class CustomerServiceTests
    {
        public ICustomerRepository Repository { get; set; }

        public CustomerService Service { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Mapper.Initialize(MapperConfiguration.Configure);
        }

        [Test]
        public void ListShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            var mockData = Builder<Customer>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<Customer>>(_ => _.List(0))
                .Returns(mockData);
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.List(0);
            var same = mockData.All(customer => result.Any(model => 
                model.FirstName == customer.FirstName
                && model.Address == customer.Address
                && model.LastName == customer.LastName
                && model.MobileNumber == customer.MobileNumber));

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void ListShouldReturnAnEmptyEnumerableIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            mock.Setup<IEnumerable<Customer>>(_ => _.List(0))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.List(0);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
        
        [Test]
        public void AddShouldReturnTrueIfRepositoryHasNoExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);
            var mockCustomer = Builder<CustomerAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockCustomer);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddShouldReturnFalseIfRepositoryHasExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(_ => _.CreateNewCustomer(It.IsAny<Customer>()))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);
            var mockCustomer = Builder<CustomerAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockCustomer);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            var mockId = Guid.NewGuid();
            var mockData = Builder<Customer>.CreateNew().Build();
            mock.Setup(_ => _.GetCustomerById(mockId))
                .Returns(mockData);
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.Get(mockId);
            bool same = mockData.Address == result.Address
                && mockData.FirstName == result.FirstName
                && mockData.LastName == result.LastName
                && mockData.MobileNumber == result.MobileNumber;

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void GetShouldReturnNullIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup(_ => _.GetCustomerById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.Get(mockId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void EditShouldReturnTrueIfRepositoryThrowsNoExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);
            var mockCustomer = Builder<CustomerEditModel>.CreateNew().Build();

            // Act
            var result = Service.Edit(mockCustomer);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditShouldReturnFalseIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(_ => _.UpdateCustomerDetails(It.IsAny<Customer>()))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);
            var mockCustomer = Builder<CustomerEditModel>.CreateNew().Build();

            // Act
            var result = Service.Edit(mockCustomer);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteShouldReturnTrueIfRepositoryThrowsNoExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            var mockId = Guid.NewGuid();
            var mockData = Builder<CustomerViewModel>.CreateNew().Build();
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteShouldReturnFalseIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<ICustomerRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup(_ => _.DeleteCustomerById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new CustomerService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}