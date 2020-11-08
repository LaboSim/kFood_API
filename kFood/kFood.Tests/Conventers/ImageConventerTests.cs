using DataModelLibrary.Conventers;
using DataModelLibrary.Conventers.Interfaces;
using Xunit;

namespace kFood.Tests.Conventers
{
    public class ImageConventerTests
    {
        [Theory]
        [InlineData("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        public void ConvertBase64ToImage_Success(string base64Photo)
        {
            // Arrange
            IImageConverter imageConverter = new ImageConverter();

            // Act
            var image = imageConverter.ConvertToImage(base64Photo);

            // Assert
            Assert.True(image != null);
        }
    }
}
