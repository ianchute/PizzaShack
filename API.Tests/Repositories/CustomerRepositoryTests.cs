using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Repositories;
using API.Services;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace API.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        public CustomerRepository Repository { get; set; }
        public IContext Context { get; set; }

        [Test]
        public void ListShouldReturnNonEmptyEnumerableIfPageIndexHasNotExceeded()
        {
            // Arrange
            const int DATA_SIZE = 100;
            var mockData = Builder<Customer>.CreateListOfSize(DATA_SIZE).Build();
            var mockDbSet = new MockDbSet<Customer>(mockData);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);
            var maxNumberOfPages = (DATA_SIZE - 1) / Constants.CUSTOMER_PAGE_SIZE;
            var availablePage = maxNumberOfPages;

            // Act
            var result = Repository.List(availablePage);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ListShouldReturnEmptyEnumerableIfPageIndexHasExceeded()
        {
            // Arrange
            const int DATA_SIZE = 100;
            var mockData = Builder<Customer>.CreateListOfSize(DATA_SIZE).Build();
            var mockDbSet = new MockDbSet<Customer>(mockData);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);
            var maxNumberOfPages = (DATA_SIZE - 1) / Constants.CUSTOMER_PAGE_SIZE;
            var exceedingPage = maxNumberOfPages + 1;

            // Act
            var result = Repository.List(exceedingPage);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void CreateNewCustomerShouldCauseDbSetToContainCustomer()
        {
            // Arrange
            var mockDbSet = new MockDbSet<Customer>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);
            var mockCustomer = Builder<Customer>.CreateNew().Build();

            // Act
            Repository.CreateNewCustomer(mockCustomer);

            // Assert
            bool contains = Context.Customers.Contains(mockCustomer);
            Assert.IsTrue(contains);
        }

        [Test]
        public void GetCustomerByIdShouldReturnSameCustomerIfCustomerExistsInDbSet()
        {
            // Arrange
            var mockCustomer = Builder<Customer>.CreateNew().Build();
            var initialList = new Customer[] { mockCustomer };
            var mockDbSet = new MockDbSet<Customer>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);

            // Act
            var result = Repository.GetCustomerById(mockCustomer.Id);

            // Assert
            Assert.AreEqual(mockCustomer, result);
        }

        [Test]
        public void GetCustomerByIdShouldReturnNullCustomerIfCustomerDoesNotExistInDbSet()
        {
            // Arrange
            var mockDbSet = new MockDbSet<Customer>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = Repository.GetCustomerById(nonExistentId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void UpdateCustomerDetailsShouldCauseCustomerInDbSetToBeUpdated()
        {
            // Arrange
            var mockCustomer = Builder<Customer>.CreateNew().Build();
            var initialList = new Customer[] { mockCustomer };
            var mockDbSet = new MockDbSet<Customer>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);
            var updates = Builder<Customer>.CreateNew().And(_ => { _.Id = mockCustomer.Id; }).Build();

            // Act
            Repository.UpdateCustomerDetails(updates);
            var updatedCustomer = mockDbSet.Find(mockCustomer.Id);

            // Assert
            bool updated = mockCustomer.Address == updatedCustomer.Address
                            && mockCustomer.FirstName == updatedCustomer.FirstName
                            && mockCustomer.Id == updatedCustomer.Id
                            && mockCustomer.LastName == updatedCustomer.LastName
                            && mockCustomer.MobileNumber == updatedCustomer.MobileNumber;
            Assert.IsTrue(updated);
        }

        [Test]
        public void DeleteCustomerByIdShouldCauseCustomerToBeNotContainedByDbSet()
        {
            // Arrange
            var mockCustomer = Builder<Customer>.CreateNew().Build();
            var initialList = new Customer[] { mockCustomer };
            var mockDbSet = new MockDbSet<Customer>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<Customer>>(_ => _.Customers)
                .SetupGet(_ => _.Customers)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new CustomerRepository(Context);

            // Act
            Repository.DeleteCustomerById(mockCustomer.Id);

            // Assert
            var contains = mockDbSet.Contains(mockCustomer);
            Assert.IsFalse(contains);
        }
    }
}
