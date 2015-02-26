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
    public class CustomersControllerTests
    {
        public CustomersController Controller { get; set; }
        public ICustomerService Service { get; set; }

        [Test]
        public void ListShouldReturnOkIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup<IEnumerable<CustomerViewModel>>(_ => _.List(0))
                .Returns(Builder<CustomerViewModel>.CreateListOfSize(100).Build());
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List(0);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void ListShouldReturnTheSameDataIfServiceListReturnsData()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            var mockContent = Builder<CustomerViewModel>.CreateListOfSize(100).Build();
            mock.Setup<IEnumerable<CustomerViewModel>>(_ => _.List(0))
                .Returns(mockContent);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.List(0);
            IEnumerable<CustomerViewModel> content;
            result.TryGetContentValue<IEnumerable<CustomerViewModel>>(out content);

            // Assert
            Assert.AreEqual(mockContent, content);
        }

        [Test]
        public void AddShouldReturnCreatedIfServiceAddReturnsTrue()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<CustomerAddModel>()))
                .Returns(true);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockCustomer = Builder<CustomerAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockCustomer);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void AddShouldReturnInternalServerErrorIfServiceAddReturnsFalse()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup<bool>(_ => _.Add(It.IsAny<CustomerAddModel>()))
                .Returns(false);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockCustomer = Builder<CustomerAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockCustomer);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Test]
        public void AddShouldReturnUnprocessableEntityIfModelStateIsInvalid()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            Controller.ModelState.AddModelError("", "");
            var mockCustomer = Builder<CustomerAddModel>.CreateNew().Build();

            // Act
            var result = Controller.Add(mockCustomer);

            // Assert
            Assert.AreEqual(422, (int)result.StatusCode);
        }

        [Test]
        public void GetByIdShouldReturnOkAndTheSameCustomerViewModelIfServiceReturnsData()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            var mockId = Guid.NewGuid();
            var mockCustomer = Builder<CustomerViewModel>.CreateNew().Build();
            mock.Setup<CustomerViewModel>(_ => _.Get(mockId))
                .Returns(mockCustomer);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);
            CustomerViewModel content;
            result.TryGetContentValue<CustomerViewModel>(out content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(mockCustomer, content);
        }

        [Test]
        public void GetByIdShouldReturnNotFoundIfServiceReturnsNull()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            var mockId = Guid.NewGuid();
            mock.Setup<CustomerViewModel>(_ => _.Get(mockId))
                .Returns<CustomerViewModel>(null);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Get(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void EditShouldReturnOKIfServiceReturnsTrue()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup<bool>(_ => _.Edit(It.IsAny<CustomerEditModel>()))
                .Returns(true);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockCustomer = Builder<CustomerEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockCustomer);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void EditShouldReturnUnprocessableEntityIfModelStateIsInvalid()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            Controller.ModelState.AddModelError("", "");
            var mockCustomer = Builder<CustomerEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockCustomer);

            // Assert
            Assert.AreEqual(422, (int)result.StatusCode);
        }

        [Test]
        public void EditShoudlReturnNotFoundIfServiceReturnsFalse()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup<bool>(_ => _.Edit(It.IsAny<CustomerEditModel>()))
                .Returns(false);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();
            var mockCustomer = Builder<CustomerEditModel>.CreateNew().Build();

            // Act
            var result = Controller.Edit(mockCustomer);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void DeleteShouldReturnNoContentIfServiceReturnsTrue()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(true);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void DeleteShouldReturnNotFoundIfServiceReturnsFalse()
        {
            // Arrange
            var mock = new Mock<ICustomerService>();
            var mockId = Guid.NewGuid();
            mock.Setup<bool>(_ => _.Delete(mockId))
                .Returns(false);
            Service = mock.Object;
            Controller = new CustomersController(Service);
            Controller.Configuration = new HttpConfiguration();
            Controller.Request = new HttpRequestMessage();

            // Act
            var result = Controller.Delete(mockId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
