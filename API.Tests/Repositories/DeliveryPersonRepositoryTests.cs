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
    public class DeliveryPersonRepositoryTests
    {
        public DeliveryPersonRepository Repository { get; set; }
        public IContext Context { get; set; }

        [Test]
        public void ListDeliveryPersonsFromRepositoryShouldReturnNonEmptyEnumerableIfPageIndexHasNotExceeded()
        {
            // Arrange
            const int DATA_SIZE = 100;
            var mockData = Builder<DeliveryPerson>.CreateListOfSize(DATA_SIZE).Build();
            var mockDbSet = new MockDbSet<DeliveryPerson>(mockData);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);
            var maxNumberOfPages = (DATA_SIZE - 1) / Constants.DELIVERY_PERSON_PAGE_SIZE;
            var availablePage = maxNumberOfPages;

            // Act
            var result = Repository.List(availablePage);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ListDeliveryPersonsFromRepositoryShouldReturnEmptyEnumerableIfPageIndexHasExceeded()
        {
            // Arrange
            const int DATA_SIZE = 100;
            var mockData = Builder<DeliveryPerson>.CreateListOfSize(DATA_SIZE).Build();
            var mockDbSet = new MockDbSet<DeliveryPerson>(mockData);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);
            var maxNumberOfPages = (DATA_SIZE - 1) / Constants.CUSTOMER_PAGE_SIZE;
            var exceedingPage = maxNumberOfPages + 1;

            // Act
            var result = Repository.List(exceedingPage);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void CreateNewDeliveryPersonShouldCauseDbSetToContainCustomer()
        {
            // Arrange
            var mockDbSet = new MockDbSet<DeliveryPerson>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);
            var mockCustomer = Builder<DeliveryPerson>.CreateNew().Build();

            // Act
            Repository.CreateNewDeliveryPerson(mockCustomer);

            // Assert
            bool contains = Context.DeliveryPersons.Contains(mockCustomer);
            Assert.IsTrue(contains);
        }

        [Test]
        public void GetCustomerByIdShouldReturnSameCustomerIfCustomerExistsInDbSet()
        {
            // Arrange
            var mockDPerson = Builder<DeliveryPerson>.CreateNew().Build();
            var initialList = new DeliveryPerson[] { mockDPerson };
            var mockDbSet = new MockDbSet<DeliveryPerson>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);

            // Act
            var result = Repository.GetDeliveryPersonById(mockDPerson.Id);

            // Assert
            Assert.AreEqual(mockDPerson, result);
        }

        [Test]
        public void GetCustomerByIdShouldReturnNullCustomerIfCustomerDoesNotExistInDbSet()
        {
            // Arrange
            var mockDbSet = new MockDbSet<DeliveryPerson>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = Repository.GetDeliveryPersonById(nonExistentId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void UpdateCustomerDetailsShouldCauseCustomerInDbSetToBeUpdated()
        {
            // Arrange
            var mockDPerson = Builder<DeliveryPerson>.CreateNew().Build();
            var initialList = new DeliveryPerson[] { mockDPerson };
            var mockDbSet = new MockDbSet<DeliveryPerson>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);
            var updates = Builder<DeliveryPerson>.CreateNew().And(_ => { _.Id = mockDPerson.Id; }).Build();

            // Act
            Repository.UpdateDeliveryPersonDetails(updates);
            var updatedCustomer = mockDbSet.Find(mockDPerson.Id);

            // Assert
            bool updated = mockDPerson.FirstName == updatedCustomer.FirstName
                            && mockDPerson.Id == updatedCustomer.Id
                            && mockDPerson.LastName == updatedCustomer.LastName;
            Assert.IsTrue(updated);
        }

        [Test]
        public void DeleteCustomerByIdShouldCauseCustomerToBeNotContainedByDbSet()
        {
            // Arrange
            var mockCustomer = Builder<DeliveryPerson>.CreateNew().Build();
            var initialList = new DeliveryPerson[] { mockCustomer };
            var mockDbSet = new MockDbSet<DeliveryPerson>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<DeliveryPerson>>(_ => _.DeliveryPersons)
                .SetupGet(_ => _.DeliveryPersons)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new DeliveryPersonRepository(Context);

            // Act
            Repository.DeleteDeliveryPersonById(mockCustomer.Id);

            // Assert
            var contains = mockDbSet.Contains(mockCustomer);
            Assert.IsFalse(contains);
        }
    }
}
