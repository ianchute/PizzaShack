using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.Services.Interfaces;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
namespace API.Controllers.Tests
{
    [TestFixture]
    public class PizzaFlavorControllerTests 
    {
        PizzaFlavorsController Controller { get; set; }
        IPizzaFlavorService Service { get; set; }

        [Test]
        public void ListFlavorsServiceShouldReturnOkIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            mock.Setup<IEnumerable<PizzaFlavorViewModel>>(_ => _.List())
                .Returns(Builder<PizzaFlavorViewModel>.CreateListOfSize(100).Build());
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void ListFlavorsShouldReturnTheSameDataIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            var mockContent = Builder<PizzaFlavorViewModel>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<PizzaFlavorViewModel>>(_ => _.List())
                .Returns(mockContent);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List();
            IEnumerable<PizzaFlavorViewModel> content;
            result.TryGetContentValue<IEnumerable<PizzaFlavorViewModel>>(out content);

            // Assert
            Assert.AreEqual(mockContent, content);
        }

        [Test]
        public void AddFlavorShouldReturnCreatedIfServiceAddReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<PizzaFlavorAddModel>()))
                .Returns(true);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockAddModel = Builder<PizzaFlavorAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockAddModel);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void AddFlavorShouldReturnInternalServerErrorIfServiceAddReturnsFalse()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<PizzaFlavorAddModel>()))
                .Returns(false);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockAddModel = Builder<PizzaFlavorAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockAddModel);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Test]
        public void AddFlavorShouldReturnUnprocessableEntityIfModelStateIsInvalid()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            Controller.ModelState.AddModelError("", "");
            var mockAddModel = Builder<PizzaFlavorAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockAddModel);

            // Assert
            Assert.AreEqual(422, (int)result.StatusCode);
        }

        [Test]
        public void GetFlavorByIdShouldReturnOkAndTheSameViewModelIfServiceReturnsData()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            var mockId = Guid.NewGuid();
            var mockViewModel = Builder<PizzaFlavorViewModel>.CreateNew().Build();
            mock.Setup<PizzaFlavorViewModel>(_ => _.Get(mockId))
                .Returns(mockViewModel);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);
            PizzaFlavorViewModel content;
            result.TryGetContentValue<PizzaFlavorViewModel>(out content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(mockViewModel, content);
        }

        [Test]
        public void GetFlavorByIdShouldReturnNotFoundIfServiceReturnsNull()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            var mockId = Guid.NewGuid();
            mock.Setup<PizzaFlavorViewModel>(_ => _.Get(mockId))
                .Returns<PizzaFlavorViewModel>(null);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void DeleteFlavorShouldReturnNoContentIfServiceReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(true);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void DeleteFlavorShouldReturnNotFoundIfServiceReturnsFalse()
        {
            // Arrange
            var mock = new Mock<IPizzaFlavorService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(false);
            Service = mock.Object;
            Controller = new PizzaFlavorsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
