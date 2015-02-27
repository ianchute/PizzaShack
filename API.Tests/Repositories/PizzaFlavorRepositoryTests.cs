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
    public class PizzaFlavorRepositoryTests
    {
        public PizzaFlavorRepository Repository { get; set; }
        public IContext Context { get; set; }

        [Test]
        public void ListFlavorsShouldReturnSameEnumerableInDbSet()
        {
            // Arrange
            const int DATA_SIZE = 100;
            var mockData = Builder<PizzaFlavor>.CreateListOfSize(DATA_SIZE).Build();
            var mockDbSet = new MockDbSet<PizzaFlavor>(mockData);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<PizzaFlavor>>(_ => _.PizzaFlavors)
                .SetupGet(_ => _.PizzaFlavors)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new PizzaFlavorRepository(Context);

            // Act
            var result = Repository.List();

            // Assert
            var same = mockData.All(_ => result.Any(model =>
                model.FlavorName == _.FlavorName));
            Assert.IsTrue(same);
        }

        [Test]
        public void CreateNewFlavorShouldCauseDbSetToContainCustomer()
        {
            // Arrange
            var mockDbSet = new MockDbSet<PizzaFlavor>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<PizzaFlavor>>(_ => _.PizzaFlavors)
                .SetupGet(_ => _.PizzaFlavors)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new PizzaFlavorRepository(Context);
            var mockFlavor = Builder<PizzaFlavor>.CreateNew().Build();

            // Act
            Repository.CreateNewFlavor(mockFlavor);

            // Assert
            bool contains = Context.PizzaFlavors.Contains(mockFlavor);
            Assert.IsTrue(contains);
        }

        [Test]
        public void GetFlavorByIdShouldReturnSameFlavorIfExistsInDbSet()
        {
            // Arrange
            var mockFlavor = Builder<PizzaFlavor>.CreateNew().Build();
            var initialList = new PizzaFlavor[] { mockFlavor };
            var mockDbSet = new MockDbSet<PizzaFlavor>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<PizzaFlavor>>(_ => _.PizzaFlavors)
                .SetupGet(_ => _.PizzaFlavors)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new PizzaFlavorRepository(Context);

            // Act
            var result = Repository.GetFlavorById(mockFlavor.Id);

            // Assert
            Assert.AreEqual(mockFlavor, result);
        }

        [Test]
        public void GetFlavorByIdShouldReturnNullFlavorIfDoesNotExistInDbSet()
        {
            // Arrange
            var mockDbSet = new MockDbSet<PizzaFlavor>();
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<PizzaFlavor>>(_ => _.PizzaFlavors)
                .SetupGet(_ => _.PizzaFlavors)
                .Returns(mockDbSet);
            Context = mockContext.Object;
            Repository = new PizzaFlavorRepository(Context);
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = Repository.GetFlavorById(nonExistentId);

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void DeleteCustomerByIdShouldCauseCustomerToBeNotContainedByDbSet()
        {
            // Arrange
            var mockFlavor = Builder<PizzaFlavor>.CreateNew().Build();
            var initialList = new PizzaFlavor[] { mockFlavor };
            var mockDbSet = new MockDbSet<PizzaFlavor>(initialList);
            var mockContext = new Mock<IContext>();
            mockContext.SetupProperty<IDbSet<PizzaFlavor>>(_ => _.PizzaFlavors)
                .SetupGet(_ => _.PizzaFlavors)
                .Returns(mockDbSet);
            mockContext.Setup(_ => _.Save())
                .Callback(mockDbSet.Save);
            Context = mockContext.Object;
            Repository = new PizzaFlavorRepository(Context);

            // Act
            Repository.DeleteFlavorById(mockFlavor.Id);

            // Assert
            var contains = mockDbSet.Contains(mockFlavor);
            Assert.IsFalse(contains);
        }
    }
}
