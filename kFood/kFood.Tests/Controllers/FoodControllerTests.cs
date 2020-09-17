using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using kFood.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class FoodControllerTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(51)]
        [InlineData(26)]
        public void GetFood_Successful(int foodId)
        {
            // Arrange
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFoodProductsDAO>()
                    .Setup(f => f.GetFoodProduct(foodId))
                    .Returns(new FoodProduct()
                    {
                        Id = foodId,
                        Name = "Example name",
                        FoodPhoto = "Example photo"
                    });

                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult actionResult = cls.GetFood(foodId);
                var contentResult = actionResult as OkNegotiatedContentResult<FoodProduct>;

                //Assert
                Assert.NotNull(contentResult);
                Assert.NotNull(contentResult.Content);
                Assert.Equal(foodId, contentResult.Content.Id);
            }
        }
    }
}
