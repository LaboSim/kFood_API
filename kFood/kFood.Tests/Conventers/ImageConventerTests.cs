using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace kFood.Tests.Conventers
{
    public class ImageConventerTests
    {
        [Theory]
        [InlineData("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        public void ConvertBase64ToImage(string base64Photo)
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
