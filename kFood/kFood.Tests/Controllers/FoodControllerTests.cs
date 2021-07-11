using Autofac.Extras.Moq;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;
using kFood.Controllers;
using kFood.Models.Interfaces;
using kFood.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class FoodControllerTests
    {
        #region GET SPECIFIC FOOD PRODUCT - Ok/NotFound/Exception
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
                        FoodImageURL = new Uri($"http://localhost:51052/getFood/{foodId}")
                    });

                var cls = mock.Create<FoodController>();
                var expeced = new FoodProduct()
                {
                    Id = foodId,
                    Name = "Food Name",
                    Description = "Sample description of food product",
                    FoodImageURL = new Uri($"http://localhost:51052/getFood/{foodId}")
                };

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<FoodProduct>;

                // Assert
                Assert.IsType<OkNegotiatedContentResult<FoodProduct>>(actualActionResult);
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
        [InlineData(4)]
        public void GetFoodProductAboutSpecificId_Exception(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(x => x.GetSpecificFoodProduct(foodId))
                    .Throws(new Exception());

                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult actualActionResult = cls.GetFood(foodId);

                // Assert
                Assert.IsType<BadRequestResult>(actualActionResult);
            }
        }
        #endregion

        #region GET FOODS - Ok/Ok_EmptyCollection
        /// <summary>
        /// UnitTest
        /// Module: Controller
        /// Description: Get result of http action related to fetch collection of foods
        /// Expected result: Ok 
        /// </summary>
        [Fact]
        public void GetFoods_Ok()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(p => p.GetFoods())
                    .Returns(UnitTestHelper.GetFoodsCollection());

                var controller = mock.Create<FoodController>();
                IEnumerable<FoodProduct> expectedFoods = UnitTestHelper.GetFoodsCollection();

                // Act
                IHttpActionResult actualActionResult = controller.GetFoods();
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<IEnumerable<FoodProduct>>;

                // Assert
                Assert.IsType<OkNegotiatedContentResult<IEnumerable<FoodProduct>>>(actualActionResult);
                Assert.NotNull(actualActionResult);
                Assert.NotNull(actualContentResult.Content);

                IList<FoodProduct> contentList = actualContentResult.Content as IList<FoodProduct>;
                IList<FoodProduct> expectedList = expectedFoods as IList<FoodProduct>;

                Assert.Equal(expectedList.Count, contentList.Count);

                for (int i=0; i< expectedList.Count; i++)
                {
                    Assert.Equal(expectedList[i].Id, contentList[i].Id);
                    Assert.Equal(expectedList[i].Name, contentList[i].Name);
                    Assert.Equal(expectedList[i].Description, contentList[i].Description);
                    Assert.Equal(expectedList[i].FoodImageURL.AbsoluteUri, contentList[i].FoodImageURL.AbsoluteUri);
                }
            }
        }

        /// <summary>
        /// UnitTest
        /// Module: Controller
        /// Description: Get result of http action related to fetch empty collection of foods
        /// Expected result: Ok 
        /// </summary>
        [Fact]
        public void GetFoods_Ok_EmptyCollection()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(p => p.GetFoods())
                    .Returns(new List<FoodProduct>());

                var controller = mock.Create<FoodController>();
                IList<FoodProduct> expectedFoods = new List<FoodProduct>();

                // Act
                IHttpActionResult actualActionResult = controller.GetFoods();
                var actualContentResult = actualActionResult as OkNegotiatedContentResult<IEnumerable<FoodProduct>>;

                // Assert
                Assert.IsType<OkNegotiatedContentResult<IEnumerable<FoodProduct>>>(actualActionResult);
                Assert.NotNull(actualActionResult);
                Assert.NotNull(actualContentResult.Content);

                IList<FoodProduct> contentList = actualContentResult.Content as IList<FoodProduct>;

                Assert.True(expectedFoods.Count == contentList.Count);
                Assert.True(contentList.Count == 0);
            }
        }
        #endregion

        #region CREATE FOOD PRODUCT - Ok/NotCreated/DTOEmpty/Exception
        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Successful(FoodProductDTO foodProductDTO)
        {
            using (var mock = AutoMock.GetLoose())
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
                Assert.Equal(expectedFoodProduct.Id, actualContentResult.Content.Id);
                Assert.Equal(expectedFoodProduct.Name, actualContentResult.Content.Name);
                Assert.Equal(expectedFoodProduct.Description, actualContentResult.Content.Description);
                Assert.Equal(Convert.ToString(expectedFoodProduct.FoodImageURL), Convert.ToString(actualContentResult.Content.FoodImageURL));
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_NotCreated(FoodProductDTO foodProductDTO)
        {
            using (var mock = AutoMock.GetLoose())
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
        public void CreateFoodProduct_DTOEmpty()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var cls = mock.Create<FoodController>();

                // Act
                IHttpActionResult httpActionResult = cls.CreateFoodProduct(null);

                // Assert
                Assert.IsType<BadRequestResult>(httpActionResult);
            }
        } 

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Exception(FoodProductDTO foodProductDTO)
        {
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductProcessor>()
                    .Setup(x => x.CreateFoodProduct(foodProductDTO))
                    .Throws(new Exception());

                var foodController = mock.Create<FoodController>();

                // Act
                IHttpActionResult httpActionResult = foodController.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.IsType<BadRequestResult>(httpActionResult);
            }
        }
        #endregion

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
                Id = 1,
                Name = foodProductDTO.Name,
                Description = foodProductDTO.Description,
                FoodImageURL = new Uri("http://www.contoso.com/") // only testing TODO: change for real scenario
            };
        }
        #endregion
    }
}
