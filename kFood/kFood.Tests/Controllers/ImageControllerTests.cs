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
                    .Returns(GetSampleByteImage(foodId));

                var cls = mock.Create<ImageController>();
                var expected = GetSampleByteImage(foodId);

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

        #region Helper methods
        /// <summary>
        /// Get byte array imitate some image
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The byte array taht imitate some image</returns>
        private byte[] GetSampleByteImage(int foodId)
        {
            return new byte[] { 255, 216 };
        } 
        #endregion
    }
}
