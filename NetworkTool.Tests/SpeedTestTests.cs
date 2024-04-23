using System.Net;
using NetworkTool.Utilities;

namespace NetworkTool.Tests
{
    [TestClass]
    public class SpeedTestTests
    {
        [TestMethod]
        public async Task Start_SetsDownloadSpeed()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act
            await speedTest.Start();

            // Assert
            Assert.IsTrue(speedTest.DownloadSpeed > 0);
        }

        [TestMethod]
        public async Task Start_UsesWebClient()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act
            await speedTest.Start();

            // Assert
            Assert.IsNotNull(speedTest.WebClient);
        }

        [TestMethod]
        public void DownloadSpeed_InitiallyZero()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act

            // Assert
            Assert.AreEqual(0, speedTest.DownloadSpeed);
        }

        [TestMethod]
        public void WebClient_InitiallyNull()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act

            // Assert
            Assert.IsNull(speedTest.WebClient);
        }

        [TestMethod]
        public async Task Start_SetsWebClient()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act
            await speedTest.Start();

            // Assert
            Assert.IsNotNull(speedTest.WebClient);
        }

        [TestMethod]
        public void Start_ThrowsIfUrlInvalid()
        {
            // Arrange
            var speedTest = new SpeedTest();

            // Act
            Task act() => speedTest.Start();

            // Assert
            Assert.ThrowsExceptionAsync<WebException>(act);
        }
    }
}