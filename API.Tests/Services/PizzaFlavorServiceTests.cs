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
    public class PizzaFlavorServiceTests 
    {
        public IPizzaFlavorRepository Repository { get; set; }

        public PizzaFlavorService Service { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Mapper.Initialize(MapperConfiguration.Configure);
        }

        [Test]
        public void ListFlavorsShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            var mockData = Builder<PizzaFlavor>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<PizzaFlavor>>(_ => _.List())
                .Returns(mockData);
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.List();
            var same = mockData.All(_ => result.Any(model =>
                model.FlavorName == _.FlavorName));

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void ListFlavorsShouldReturnEmptyEnumerableIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            mock.Setup<IEnumerable<PizzaFlavor>>(_ => _.List())
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.List();

            // Assert
            Assert.AreEqual(0, result.Count());
        }
        
        [Test]
        public void AddFlavorShouldReturnTrueIfRepositoryHasNoExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);
            var mockCustomer = Builder<PizzaFlavorAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockCustomer);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddFlavorShouldReturnFalseIfRepositoryHasExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            mock.Setup(_ => _.CreateNewFlavor(It.IsAny<PizzaFlavor>()))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);
            var mockAddModel = Builder<PizzaFlavorAddModel>.CreateNew().Build();

            // Act
            var result = Service.Add(mockAddModel);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetFlavorByIdShouldReturnTheSameDataIfRepositoryReturnsData()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            var mockId = Guid.NewGuid();
            var mockData = Builder<PizzaFlavor>.CreateNew().Build();
            mock.Setup<PizzaFlavor>(_ => _.GetFlavorById(mockId))
                .Returns(mockData);
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.Get(mockId);
            bool same = mockData.FlavorName == result.FlavorName;

            // Assert
            Assert.IsTrue(same);
        }

        [Test]
        public void GetFlavorByIdShouldReturnNullIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup<PizzaFlavor>(_ => _.GetFlavorById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.Get(mockId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void DeleteFlavorByIdShouldReturnTrueIfRepositoryThrowsNoExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            var mockId = Guid.NewGuid();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteFlavorByIdShouldReturnFalseIfRepositoryThrowsExceptions()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorRepository>();
            var mockId = Guid.NewGuid();
            mock.Setup(_ => _.DeleteFlavorById(mockId))
                .Throws<Exception>();
            Repository = mock.Object;
            Service = new PizzaFlavorService(Repository);

            // Act
            var result = Service.Delete(mockId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}