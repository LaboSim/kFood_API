﻿using Autofac.Extras.Moq;
using kFood.Controllers;
using kFood.Models.Foods;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class FoodControllerTests
    {
        [Theory]
        [InlineData(5)]
        public void GetFood_Successful(int foodId)
        {
            // Arrange
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFoodProductDAO>()
                    .Setup(f => f.GetFoodProduct(foodId))
                    .Returns(GetSampleFoodProduct(foodId));

                var controller = new FoodController();

                // Act
                IHttpActionResult actionResult = controller.GetFood(foodId);
                var contentResult = actionResult as OkNegotiatedContentResult<FoodProduct>;

                // Assert
                Assert.NotNull(contentResult);
                Assert.NotNull(contentResult.Content);
                Assert.Equal(foodId, contentResult.Content.Id);
            }
        }

        private FoodProduct GetSampleFoodProduct(int foodId)
        {
            FoodProduct foodProduct = new FoodProduct()
            {
                Name = "Lasagne Bolognese",
                FoodPhoto = "test"
            };

            return foodProduct;
        }
    }
}
