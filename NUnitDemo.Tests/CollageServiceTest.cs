using Moq;
using NUnit.Framework;
using NUnitDemo.Core;

namespace NUnitDemo.Tests
{
    [TestFixture]
    public class CollageServiceTest
    {
        private Mock<ICollageService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ICollageService>();
        }

        [Test]
        public void GetWelcomeNote_Using_Mock()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetWelcomeNote("Ali"))
                .Returns("Mock Welcome Ali");

            // Act
            var result = _mockService.Object.GetWelcomeNote("Ali");

            // Assert
            Assert.That(result, Is.EqualTo("Mock Welcome Ali"));
        }

        [Test]
        public void GetFareWellNote_Using_Mock()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetFareWellNote("Ali"))
                .Returns("Mock Farewell Ali");

            // Act
            var result = _mockService.Object.GetFareWellNote("Ali");

            // Assert
            Assert.That(result, Is.EqualTo("Mock Farewell Ali"));
        }
    }
}
