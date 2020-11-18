using Autofac.Extras.Moq;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using BusinessLogicLibrary.Images;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Xunit;

namespace kFood.Tests.Handlers
{
    public class ImageHandlerTests
    {
        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_Success(string base64Image)
        {
            string tempPath = ConfigurationManager.AppSettings["PathToTemporaryImage"];

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}{Path.GetRandomFileName().Replace(".", "")}");

                var cls = mock.Create<ImageHandler>();

                // Act
                var pathImage = cls.SaveImageTemporarily(base64Image);

                // Assert
                Assert.True(File.Exists($"{pathImage}"));
            }
        }

        #region Helper methods
        /// <summary>
        /// Get sample image 
        /// </summary>
        /// <returns>The collection of fake BASE64 string</returns>
        public static IEnumerable<object[]> GetSampleImage()
        {
            return new List<object[]>
            {
                new object[]
                {
                    GetSampleImageAsBase64()
                }
            };
        }

        /// <summary>
        /// Get sample BASE64 image from file
        /// </summary>
        /// <returns>The image as BASE64</returns>
        private static string GetSampleImageAsBase64()
        {
            string testImage = ConfigurationManager.AppSettings["TestImage"];

            return File.ReadAllText(testImage);
        }
        #endregion
    }
}
