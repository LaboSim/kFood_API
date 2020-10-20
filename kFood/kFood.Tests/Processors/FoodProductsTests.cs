using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using kFood.Models;
using System;
using Xunit;

namespace kFood.Tests.Processors
{
    public class FoodProductsTests
    {
        [Theory]
        [InlineData(7)]
        public void GetSpecificFoodProduct_Success(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.GetFoodProduct(foodId))
                    .Returns(GetSampleFoodProduct(foodId));

                var cls = mock.Create<FoodProductProcessor>();
                var expected = GetSampleFoodProduct(foodId);

                // Act
                var foodProduct = cls.GetSpecificFoodProduct(foodId);

                // Assert
                Assert.True(foodProduct != null);
                Assert.Equal(expected.Id, foodProduct.Id);
                Assert.Equal(expected.Name, foodProduct.Name);
                Assert.Equal(expected.FoodImageURL, foodProduct.FoodImageURL);
            }
        }

        #region Helper methods
        /// <summary>
        /// Get sample food product
        /// </summary>
        /// <param name="foodId">The fake identifier of food product</param>
        /// <returns>The instance of <see cref="FoodProduct"/></returns>
        private FoodProduct GetSampleFoodProduct(int foodId)
        {
            return new FoodProduct()
            {
                Id = foodId,
                Name = "Sample food product",
                FoodImageURL = new Uri("http://localhost:51052/view/foodproductimage/1")
            };
        }
        #endregion
    }
}
