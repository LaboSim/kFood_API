using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using kFood.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.DAO
{
    public class FoodProductsTests
    {
        [Theory]
        [InlineData(4)]
        public void GetFoodProductAboutSpecificId_Result_Ok(int foodId)
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.GetFoodProduct(foodId))
                    .Returns(new FoodProduct()
                    {
                        Id = foodId,
                        Name = "Food Name",
                        FoodURL = new System.Uri("http://localhost:51052/getFood/1")
                    });

                var cls = mock.Create<FoodController>();
                var expeced = new FoodProduct()
                {
                    Id = foodId,
                    Name = "Food Name",
                    FoodURL = new System.Uri("http://localhost:51052/getFood/1")
                };

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<FoodProduct>;

                // Assert
                Assert.True(actualActionResult != null);
                Assert.True(actualContentResult.Content != null);
                Assert.Equal(expeced.Id, actualContentResult.Content.Id);
                Assert.Equal(expeced.Name, actualContentResult.Content.Name);
                Assert.Equal(expeced.FoodURL.AbsoluteUri, actualContentResult.Content.FoodURL.AbsoluteUri);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetFoodProductAboutSpecificId_Result_NotFound(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.GetFoodProduct(foodId))
                    .Returns((FoodProduct)null);

                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);

                // Assert
                Assert.IsType<NotFoundResult>(actualActionResult);
            }
        }
    }
}
