using System.Net;
using Cereal.Controllers;
using Cereal.Models;
using Cereal.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cereal.Test
{
    [TestClass]
    public class CerealControllerTests
    {
        private static CerealController GetController(Mock<ICerealService> cerealService) =>
        new(cerealService.Object);

        [TestMethod]
        public async Task GetAllCereals()
        {
            // Arrange
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.GetAllCereals())
                .ReturnsAsync([]); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.GetAllCereals();

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(actual);
            cerealService.VerifyAll();
        }

        [TestMethod]
        public async Task GetCerealById()
        {
            // Arrange
            int id = 101;
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.GetCerealById(id))
                .ReturnsAsync(new Models.CerealEntity()); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.GetCerealById(id);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(actual);
            cerealService.VerifyAll();
        }

        [TestMethod]
        public async Task GetFilteredCereals_Predicate()
        {
            // Arrange
            string predicate = "0 < Fat";
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.GetFilteredCereals_Predicate(predicate))
                .ReturnsAsync([]); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.GetFilteredCereals_Predicate(predicate);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(actual);
            cerealService.VerifyAll();
        }

        [TestMethod]
        public async Task GetImageById()
        {
            // Arrange
            int id = 101;
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.GetImageById(id, @"Cereal Pictures\"))
                .ReturnsAsync(new FileStream(@"Test", FileMode.Create)); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.GetImageById(id);

            // Assert
            Assert.IsInstanceOfType<FileResult>(actual);
            cerealService.VerifyAll();
        }

        [TestMethod]
        public async Task CreateOrUpdateCereal()
        {
            // Arrange
            CerealEntity entity = new CerealEntity();
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.CreateOrUpdateCereal(entity))
                .ReturnsAsync(101); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.CreateOrUpdateCereal(entity);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(actual);
            cerealService.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteCerealById()
        {
            // Arrange
            int id = 101;
            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.DeleteCerealById(id))
                .ReturnsAsync(HttpStatusCode.NoContent); // Doesn't matter.

            var controller = GetController(cerealService);

            // Act
            var actual = await controller.DeleteCerealById(id);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(actual);
            cerealService.VerifyAll();
        }
    }
}
