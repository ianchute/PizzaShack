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
    public class DeliveryPersonServiceTests
    {
        public IDeliveryPersonRepository Repository { get; set; }

        public DeliveryPersonService Service { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Mapper.Initialize(MapperConfiguration.Configure);
        }

        [Test]
        public void ListDeliveryPersonShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            var mockData = Builder<DeliveryPerson>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<DeliveryPerson>>(_ => _.List(0))
                .Returns(mockData);
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.List(0);
            var same = mockData.All(dPerson => result.Any(model =>
                model.FirstName == dPerson.FirstName
                && model.LastName == dPerson.LastName));

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void ListDeliveryPersonsShouldReturnEmptyEnumerableIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            mock.Setup<IEnumerable<DeliveryPerson>>(_ => _.List(0))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.List(0);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
        
        [Test]
        public void AddShouldReturnTrueIfRepositoryHasNoExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);
            var mockCustomer = Builder<DeliveryPersonAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockCustomer);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddShouldReturnFalseIfRepositoryHasExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            mock.Setup(_ => _.CreateNewDeliveryPerson(It.IsAny<DeliveryPerson>()))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);
            var mockDPerson = Builder<DeliveryPersonAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockDPerson);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            var mockId = Guid.NewGuid();
            var mockData = Builder<DeliveryPerson>.CreateNew().Build();
            mock.Setup<DeliveryPerson>(_ => _.GetDeliveryPersonById(mockId))
                .Returns(mockData);
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.Get(mockId);
            bool same = mockData.FirstName == result.FirstName
                && mockData.LastName == result.LastName;

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void GetShouldReturnNullIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup<DeliveryPerson>(_ => _.GetDeliveryPersonById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.Get(mockId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void EditShouldReturnTrueIfRepositoryThrowsNoExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);
            var mockDPerson = Builder<DeliveryPersonEditModel>.CreateNew().Build();

            // Act
            var result = Service.Edit(mockDPerson);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditShouldReturnFalseIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            mock.Setup(_ => _.UpdateDeliveryPersonDetails(It.IsAny<DeliveryPerson>()))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);
            var mockCustomer = Builder<DeliveryPersonEditModel>.CreateNew().Build();

            // Act
            var result = Service.Edit(mockCustomer);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteShouldReturnTrueIfRepositoryThrowsNoExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            var mockId = Guid.NewGuid();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteShouldReturnFalseIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup(_ => _.DeleteDeliveryPersonById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new DeliveryPersonService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}