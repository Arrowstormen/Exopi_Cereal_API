using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cereal.Data;
using Cereal.Models;
using Cereal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Identity;
using Moq;
using Moq.EntityFrameworkCore;

namespace Cereal.Test
{
    [TestClass]
    public class CerealServiceTests
    {
        private static CerealService GetService(ICerealContext context) => new CerealService(context);

        [TestMethod]
        public async Task GetAllCereals_Multiple()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran"
            };
            var cereal2 = new CerealEntity
            {
                Name = "Bran 100%"
            };
            var cereal3 = new CerealEntity
            {
                Name = "Bran 200%"
            };
            IEnumerable<CerealEntity> expected = new [] {cereal1, cereal2, cereal3};
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1, cereal2, cereal3]); 

            var service = GetService(context.Object);

            // Act
            var actual = await service.GetAllCereals();

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }

        [TestMethod]
        public async Task GetAllCereals_None()
        {
            // Arrange
            IEnumerable<CerealEntity> expected = [];
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([]);
            var service = GetService(context.Object);

            // Act
            var actual = await service.GetAllCereals();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetCerealById_Invalid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 102
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.GetCerealById(101));

            // Assert
            Assert.AreEqual("No cereal with id 101 currently exists.", ex.Message);
        }

        [TestMethod]
        public async Task GetCerealById_Valid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 101
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var actual = await service.GetCerealById(101);

            // Assert
            Assert.AreEqual(cereal1, actual);
        }

        [TestMethod]
        public async Task GetFilteredCereals_Predicate_Simple()
        {
            // Arrange
            string predicate = "Fat == 0";
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Fat = 10
            };
            var cereal2 = new CerealEntity
            {
                Name = "Bran 100%",
                Fat = 0
            };
            var cereal3 = new CerealEntity
            {
                Name = "Bran 200%",
                Fat = 0
            };
            IEnumerable<CerealEntity> expected = new[] { cereal2, cereal3 };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1, cereal2, cereal3]);

            var service = GetService(context.Object);

            // Act
            var actual = await service.GetFilteredCereals_Predicate(predicate);

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }

        [TestMethod]
        public async Task GetFilteredCereals_Predicate_AndOperator()
        {
            // Arrange
            string predicate = "Fat == 0 && 100 < Calories";
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Fat = 10,
                Calories = 125
            };
            var cereal2 = new CerealEntity
            {
                Name = "Bran 100%",
                Fat = 0,
                Calories = 150,
            };
            var cereal3 = new CerealEntity
            {
                Name = "Bran 200%",
                Fat = 0,
                Calories = 85
            };
            IEnumerable<CerealEntity> expected = new[] { cereal2 };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1, cereal2, cereal3]);

            var service = GetService(context.Object);

            // Act
            var actual = await service.GetFilteredCereals_Predicate(predicate);

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }

        [TestMethod]
        public async Task GetImageById_InvalidId()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 102
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.GetImageById(101));

            // Assert
            Assert.AreEqual("No cereal with id 101 currently exists.", ex.Message);
        }

        [TestMethod]
        public async Task GetImageById_InvalidName()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Brand",
                Id = 101
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);
            string directory = @"..\..\..\..\Cereal\Cereal Pictures\";

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.GetImageById(101, directory));

            // Assert
            Assert.AreEqual("No image with name Brand currently exists.", ex.Message);
        }

        [TestMethod]
        public async Task GetImageById_Valid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "100% Bran",
                Id = 101
            };
            string directory = @"..\..\..\..\Cereal\Cereal Pictures\";
            var path = Directory.EnumerateFiles(directory, searchPattern: "*" + cereal1.Name + "*").FirstOrDefault();
            var expected = File.OpenRead(path);
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var actual = await service.GetImageById(101, directory);

            // Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public async Task CreateOrUpdateCereal_UpdateInvalid()
        {
            // Arrange
            var cereal_insert = new CerealEntity
            {
                Name = "Bran",
                Id = 101
            };
            var cereal_data = new CerealEntity
            {
                Name = "Bran",
                Id = 102
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal_data]);
            var service = GetService(context.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.CreateOrUpdateCereal(cereal_insert));

            // Assert
            Assert.AreEqual("No cereal with id 101 currently exists. New cereals cannot include an id.", ex.Message);
        }

        [TestMethod]
        public async Task CreateOrUpdateCereal_UpdateValid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 101,
                Manufacturer = Models.Types.Manufacturer.A,
                Type = Models.Types.TempType.H,
                Calories = 50,
                Carbo = 0,
                Cups = 1,
                Fat = 0,
                Fiber = 10,
                Potass = 0,
                Protein = 0,
                Weight = 2,
                Shelf = 0,
                Sodium = 0,
                Vitamins = 0,
                Sugars = 0,
                Rating = (float)65.21
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var actual = await service.CreateOrUpdateCereal(cereal1);

            // Assert
            Assert.AreEqual(101, actual);
        }

        [TestMethod]
        public async Task DeleteCerealById_Invalid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 102
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.DeleteCerealById(101));

            // Assert
            Assert.AreEqual("No cereal with id 101 currently exists.", ex.Message);
        }

        [TestMethod]
        public async Task DeleteCerealById_Valid()
        {
            // Arrange
            var cereal1 = new CerealEntity
            {
                Name = "Bran",
                Id = 101
            };
            var context = new Mock<ICerealContext>();
            context.Setup(x => x.Cereals).ReturnsDbSet([cereal1]);
            var service = GetService(context.Object);

            // Act
            var actual = await service.DeleteCerealById(101);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, actual);
        }

    }
}
