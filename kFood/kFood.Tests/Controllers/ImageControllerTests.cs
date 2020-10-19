using Autofac.Extras.Moq;
using kFood.Controllers;
using kFood.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class ImageControllerTests
    {
        [Theory]
        [InlineData(4)]
        public void GetMainImageForFoodProduct_Successful(int foodId)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arange
                mock.Mock<IImageProcessor>()
                    .Setup(x => x.GetMainImageForSpecificFoodProduct(foodId))
                    .Returns(GetSampleByteImage());

                var cls = mock.Create<ImageController>();
                var expected = GetSampleByteImage();

                // Act
                IHttpActionResult actualActionResult = cls.GetFoodProductMainImage(foodId);
                var actualContentResult = actualActionResult as ResponseMessageResult;

                // Assert
                Assert.True(actualContentResult != null);
                Assert.True(actualContentResult.Response != null);
                Assert.IsType<ResponseMessageResult>(actualContentResult);
                Assert.Equal(HttpStatusCode.OK, actualContentResult.Response.StatusCode);
                Assert.Equal("image/png", actualContentResult.Response.Content.Headers.ContentType.MediaType);
                Assert.IsType<ByteArrayContent>(actualContentResult.Response.Content);
            }
        }

        [Theory]
        [InlineData(3)]
        public void GetMainImageForFoodProduct_NotFound(int foodId)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arange
                mock.Mock<IImageProcessor>()
                    .Setup(x => x.GetMainImageForSpecificFoodProduct(foodId))
                    .Returns(GetEmptyByteImage());

                var cls = mock.Create<ImageController>();

                // Act
                IHttpActionResult actualActionResult = cls.GetFoodProductMainImage(foodId);
                var actualContentResult = actualActionResult as ResponseMessageResult;

                // Assert
                Assert.True(actualContentResult != null);
                Assert.True(actualContentResult.Response != null);
                Assert.IsType<ResponseMessageResult>(actualContentResult);
                Assert.Equal(HttpStatusCode.NotFound, actualContentResult.Response.StatusCode);
            }
        }

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
