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
    public class DeliveryPersonControllerTests
    {
        public DeliveryPersonsController Controller { get; set; }
        public IDeliveryPersonService Service { get; set; }

        [Test]
        public void ListDeliveryPersonsShouldReturnOkIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            mock.Setup<IEnumerable<DeliveryPersonViewModel>>(_ => _.List(0))
                .Returns(Builder<DeliveryPersonViewModel>.CreateListOfSize(100).Build());
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List(0);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void ListDeliveryPersonsShouldReturnTheSameDataIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            var mockContent = Builder<DeliveryPersonViewModel>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<DeliveryPersonViewModel>>(_ => _.List(0))
                .Returns(mockContent);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List(0);
            IEnumerable<DeliveryPersonViewModel> content;
            result.TryGetContentValue<IEnumerable<DeliveryPersonViewModel>>(out content);

            // Assert
            Assert.AreEqual(mockContent, content);
        }

        [Test]
        public void AddDeliveryPersonShouldReturnCreatedIfServiceAddReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<DeliveryPersonAddModel>()))
                .Returns(true);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockDeliveryPerson = Builder<DeliveryPersonAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void AddDeliveryPersonShouldReturnInternalServerErrorIfServiceAddReturnsFalse()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<DeliveryPersonAddModel>()))
                .Returns(false);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockDeliveryPerson = Builder<DeliveryPersonAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Test]
        public void AddDeliveryPersonShouldReturnUnprocessableEntityIfModelStateIsInvalid()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            Controller.ModelState.AddModelError("", "");
            var mockDeliveryPerson = Builder<DeliveryPersonAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(422, (int)result.StatusCode);
        }

        [Test]
        public void GetDeliveryPersonByIdShouldReturnOkAndTheSameViewModelIfServiceReturnsData()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            var mockId = Guid.NewGuid();
            var mockCustomer = Builder<DeliveryPersonViewModel>.CreateNew().Build();
            mock.Setup<DeliveryPersonViewModel>(_ => _.Get(mockId))
                .Returns(mockCustomer);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);
            DeliveryPersonViewModel content;
            result.TryGetContentValue<DeliveryPersonViewModel>(out content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(mockCustomer, content);
        }

        [Test]
        public void GetDeliveryPersonByIdShouldReturnNotFoundIfServiceReturnsNull()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            var mockId = Guid.NewGuid();
            mock.Setup<DeliveryPersonViewModel>(_ => _.Get(mockId))
                .Returns<DeliveryPersonViewModel>(null);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void EditDeliveryPersonShouldReturnOKIfServiceReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            mock.Setup<bool>(_ => _.Edit(It.IsAny<DeliveryPersonEditModel>()))
                .Returns(true);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockDeliveryPerson = Builder<DeliveryPersonEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void EditDeliveryPersonShouldReturnUnprocessableEntityIfModelStateIsInvalid()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            Controller.ModelState.AddModelError("", "");
            var mockDeliveryPerson = Builder<DeliveryPersonEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(422, (int)result.StatusCode);
        }

        [Test]
        public void EditDeliveryPersonShouldReturnNotFoundIfServiceReturnsFalse()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            mock.Setup<bool>(_ => _.Edit(It.IsAny<DeliveryPersonEditModel>()))
                .Returns(false);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockDeliveryPerson = Builder<DeliveryPersonEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockDeliveryPerson);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void DeleteDeliveryPersonShouldReturnNoContentIfServiceReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(true);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void DeleteDeliveryPersonShouldReturnNotFoundIfServiceReturnsFalse()
        {
            // Arrange
            var mock = new Mock<IDeliveryPersonService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(false);
            Service = mock.Object;
            Controller = new DeliveryPersonsController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
