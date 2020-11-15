using Autofac.Extras.Moq;
using BusinessLogicLibrary.Images;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Xunit;

namespace kFood.Tests.Handlers
{
    public class ImageHandlerTests
    {
        [Theory]
        [MemberData(nameof(GetSampleImage))]
        public void SaveImage_Success(Image image)
        {
            string tempPath = @"D:\Szymon\Temporary\";

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.GetTemporaryPath())
                    .Returns(tempPath);

                var cls = mock.Create<ImageHandler>();

                // Act
                var imageFile = cls.PlaceImageInFolder(image);

                // Assert
                Assert.True(File.Exists($@"{tempPath}{imageFile}.png"));
            }
        }

        #region Helper methods
        /// <summary>
        /// Get sample image instance
        /// </summary>
        /// <returns>The collection of fake instance <see cref="Image"/></returns>
        public static IEnumerable<object[]> GetSampleImage()
        {
            return new List<object[]>
            {
                new object[]
                {
                    CreateSampleImage()
                }
            };
        }

        /// <summary>
        /// Convert image as BASE64 string to image
        /// </summary>
        /// <returns>The instance of <see cref="Image"/></returns>
        private static Image CreateSampleImage()
        {
            string base64Image = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw=="; // this image is a single pixel (black)

            byte[] byteImage = Convert.FromBase64String(base64Image);

            Image image;
            using(MemoryStream ms = new MemoryStream(byteImage))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        #endregion
    }
}
