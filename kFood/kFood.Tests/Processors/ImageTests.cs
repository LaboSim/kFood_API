using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using kFood.Models;
using Xunit;

namespace kFood.Tests.Processors
{
    public class ImageTests
    {
        #region GET food product image - Success/EmptyImage
        [Theory]
        [InlineData(3)]
        public void GetFoodProductMainImage_Success(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageDAO>()
                    .Setup(x => x.GetFoodProductMainImage(foodId))
                    .Returns(GetSampleByteImage());

                var cls = mock.Create<ImageProcessor>();
                var expected = GetSampleByteImage();

                // Act
                byte[] image = cls.GetMainImageForSpecificFoodProduct(foodId);

                // Assert
                Assert.True(image != null);
                Assert.True(image.Length > 0);
                Assert.Equal(expected, image);
            }
        }

        [Theory]
        [InlineData(6)]
        public void GetFoodProductMainImage_EmptyArrayImage(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageDAO>()
                    .Setup(x => x.GetFoodProductMainImage(foodId))
                    .Returns(GetEmptyByteImage());

                var cls = mock.Create<ImageProcessor>();
                var expected = GetEmptyByteImage();

                // Act
                byte[] image = cls.GetMainImageForSpecificFoodProduct(foodId);

                // Assert
                Assert.True(image != null);
                Assert.True(image.Length == 0);
                Assert.Equal(expected, image);
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Get byte array imitate some image
        /// </summary>
        /// <returns>The byte array that imitate some image</returns>
        private byte[] GetSampleByteImage()
        {
            return new byte[] { 255, 216 };
        }

        /// <summary>
        /// Get empty byte array imitate lack of image
        /// </summary>
        /// <returns>The empty byte array which imitate lack of image</returns>
        private byte[] GetEmptyByteImage()
        {
            return new byte[] { };
        }
        #endregion
    }
}
