using Autofac.Extras.Moq;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using BusinessLogicLibrary.Images;
using DataModelLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Xunit;

namespace kFood.Tests.Handlers
{
    public class ImageHandlerTests
    {
        #region SAVE image as temporary file - Success/InvalidBase64/EmptyTempPath/GetTempPathNullReferenceException/GetTempPathException/DirectoryNotExistException
        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_Success(string base64Image)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}");

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                var pathImage = imageHandler.SaveImageTemporarily(base64Image);

                // Assert
                Assert.True(File.Exists($"{pathImage}"));
            }
        }

        [Theory]
        [InlineData("Testi23ngImage1213Testi23ngImage1213=")]
        public void SaveImage_InvalidBase64(string invalidBase64Image)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}");

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                string imagePath = imageHandler.SaveImageTemporarily(invalidBase64Image);

                // Assert
                Assert.True(string.IsNullOrEmpty(imagePath));
            }
        }

        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_EmptyTempPath(string base64Image)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns(string.Empty);

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                Action actionSaveImage = () => imageHandler.SaveImageTemporarily(base64Image);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionSaveImage);
                Assert.NotNull(ex);
                Assert.Equal(MessageContainer.EmptyPath, ex.Message);
            }
        }

        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_GetTempPathNullReferenceException(string base64Image)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Throws(new NullReferenceException());

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                Action actionSaveImage = () => imageHandler.SaveImageTemporarily(base64Image);

                // Assert
                NullReferenceException ex = Assert.Throws<NullReferenceException>(actionSaveImage);
                Assert.NotNull(ex);
            }
        }

        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_GetTempPathException(string base64Image)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Throws(new Exception());

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                Action actionSaveImage = () => imageHandler.SaveImageTemporarily(base64Image);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionSaveImage);
                Assert.NotNull(ex);
            }
        }

        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_DirectoryNotExistException(string base64Image)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}\\NotExistingFolder\\");

                var imageHandler = mock.Create<ImageHandler>();

                // Act
                Action actionSaveImage = () => imageHandler.SaveImageTemporarily(base64Image);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionSaveImage);
                Assert.NotNull(ex);
            }
        }
        #endregion

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
