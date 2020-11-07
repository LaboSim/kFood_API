using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;
using kFood.Controllers;
using kFood.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class FoodControllerTests
    {
        [Theory]
        [InlineData(4)]
        public void GetFoodProductAboutSpecificId_Result_Ok(int foodId)
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFoodProductProcessor>()
                    .Setup(x => x.GetSpecificFoodProduct(foodId))
                    .Returns(new FoodProduct()
                    {
                        Id = foodId,
                        Name = "Food Name",
                        Description = "Sample description of food product",
                        FoodImageURL = new Uri("http://localhost:51052/getFood/1")
                    });

                var cls = mock.Create<FoodController>();
                var expeced = new FoodProduct()
                {
                    Id = foodId,
                    Name = "Food Name",
                    Description = "Sample description of food product",
                    FoodImageURL = new Uri("http://localhost:51052/getFood/1")
                };

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<FoodProduct>;

                // Assert
                Assert.True(actualActionResult != null);
                Assert.True(actualContentResult.Content != null);
                Assert.Equal(expeced.Id, actualContentResult.Content.Id);
                Assert.Equal(expeced.Name, actualContentResult.Content.Name);
                Assert.Equal(expeced.Description, actualContentResult.Content.Description);
                Assert.Equal(expeced.FoodImageURL.AbsoluteUri, actualContentResult.Content.FoodImageURL.AbsoluteUri);
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

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Successful(FoodProductDTO foodProductDTO)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(x => x.CreateFoodProduct(foodProductDTO))
                    .Returns(CreateSampleFoodProduct(foodProductDTO));

                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult httpActionResult = cls.CreateFoodProduct(foodProductDTO);
                var actualContentResult = httpActionResult as CreatedNegotiatedContentResult<FoodProduct>;

                var expectedFoodProduct = CreateSampleFoodProduct(foodProductDTO);

                // Assert
                Assert.True(actualContentResult != null);
                Assert.True(actualContentResult.Content != null);
                Assert.Equal(Convert.ToString(expectedFoodProduct.FoodImageURL), Convert.ToString(actualContentResult.Content.FoodImageURL));
                Assert.Equal(expectedFoodProduct.Name, actualContentResult.Content.Name);
                Assert.Equal(expectedFoodProduct.Description, actualContentResult.Content.Description);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Conflict(FoodProductDTO foodProductDTO)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(x => x.CreateFoodProduct(foodProductDTO))
                    .Returns((FoodProduct)null);

                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult httpActionResult = cls.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.IsType<ConflictResult>(httpActionResult);
            }
        }

        [Fact]
        public void CreateFoodProduct_BadRequest()
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult httpActionResult = cls.CreateFoodProduct(null);

                // Assert
                Assert.IsType<BadRequestResult>(httpActionResult);
            }
        }

        #region Helper methods
        /// <summary>
        /// Create collection of <see cref="FoodProductDTO"/> to pass to controller as argument
        /// </summary>
        /// <returns>The collection of instance <see cref="FoodProductDTO"/> that imitate POST request</returns>
        public static IEnumerable<object[]> CreateFoodProductToPass()
        {
            return new List<object[]>
            {
                new object[] { new FoodProductDTO()
                {
                    Name = "Food product test name 1",
                    Description = "Sample description of food product",
                    FoodProductImage = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==" // this image is a single pixel (black)
                }}
            };
        }

        private FoodProduct CreateSampleFoodProduct(FoodProductDTO foodProductDTO)
        {
            return new FoodProduct()
            {
                Name = foodProductDTO.Name,
                Description = foodProductDTO.Description,
                FoodImageURL = new Uri("http://www.contoso.com/") // only testing TODO: change for real scenario
            };
        }
        #endregion
    }
}
