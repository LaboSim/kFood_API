using Autofac.Extras.Moq;
using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;
using kFood.App_Start;
using kFood.Models;
using Moq;
using System;
using System.Collections.Generic;
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

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Success(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(true);

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                FoodProduct addedFoodProduct = cls.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.True(addedFoodProduct != null);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Unsuccess(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();

            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(false);

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                FoodProduct foodProduct = cls.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.Null(foodProduct);
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

        /// <summary>
        /// Create collection of <see cref="FoodProductDTO"/> to pass to processor as argument
        /// </summary>
        /// <returns>The collection of fake instance <see cref="FoodProductDTO"/></returns>
        public static IEnumerable<object[]> CreateFoodProductToPass()
        {
            return new List<object[]>
            {
                new object[] { new FoodProductDTO() { Name = "Food product test name 1" } }
            };
        }
        #endregion
    }
}
