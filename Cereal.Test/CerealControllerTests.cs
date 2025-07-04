using Cereal.Controllers;
using Cereal.Services;
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

            // Act

            // Assert
        }
    }
}
