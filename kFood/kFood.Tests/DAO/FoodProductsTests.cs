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
        public void GetFoodProductAboutSpecificId_Success(int foodId)
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
                        FoodPhoto = "Food photo"
                    });

                var cls = mock.Create<FoodController>();
                var expeced = new FoodProduct()
                {
                    Id = foodId,
                    Name = "Food Name",
                    FoodPhoto = "Food photo"
                };

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<FoodProduct>;

                // Assert
                Assert.True(actualActionResult != null);
                Assert.True(actualContentResult.Content != null);
                Assert.Equal(expeced.Id, actualContentResult.Content.Id);
                Assert.Equal(expeced.Name, actualContentResult.Content.Name);
                Assert.Equal(expeced.FoodPhoto, actualContentResult.Content.FoodPhoto);
            }
        }
    }
}
