﻿using Autofac.Extras.Moq;
using AutoMapper;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using BusinessLogicLibrary.Images.Interfaces;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Messages;
using DataModelLibrary.Models.Foods;
using kFood.App_Start;
using kFood.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using Xunit;

namespace kFood.Tests.Processors
{
    public class FoodProductsTests
    {
        #region GET SPECIFIC FOOD PRODUCT - Ok/NotFound/Exception
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
                Assert.Equal(expected.Description, foodProduct.Description);
                Assert.Equal(expected.FoodImageURL, foodProduct.FoodImageURL);
            }
        }

        [Theory]
        [InlineData(7)]
        public void GetSpecificFoodProduct_NotFound(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.GetFoodProduct(foodId))
                    .Returns((FoodProduct)null);

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                var actualFoodProduct = cls.GetSpecificFoodProduct(foodId);

                // Assert
                Assert.Null(actualFoodProduct);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetSpecificFoodProduct_Exception(int foodId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.GetFoodProduct(foodId))
                    .Throws(new Exception());

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                var ex = Record.Exception(() => cls.GetSpecificFoodProduct(foodId));

                // Assert
                Assert.IsType<Exception>(ex);
            }
        }
        #endregion

        #region CREATE FOOD PRODUCT - Created/SaveImageTemporarilyTempPathEmpty/SaveImageTemporarilyDirectoryNotExist/CreateFoodProduct_CreateURIToSpecificPhoto_NullReferenceException/CreateFoodProduct_CreateURIToSpecificPhoto_Exception/NotCreated
        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_Created(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();
            int mockID = 1;

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(mockID);

                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.CreateURIToSpecificPhoto(mockID))
                    .Returns(string.Concat("http://localhost:51052/", ConfigurationManager.AppSettings["RouteToImage"], Convert.ToString(mockID)));

                mock.Mock<IImageHandler>()
                   .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                   .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}{Path.GetRandomFileName().Replace(".", "")}");

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                FoodProduct addedFoodProduct = cls.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.True(addedFoodProduct != null);
                Assert.Equal(foodProductDTO.Name, addedFoodProduct.Name);
                Assert.Equal(foodProductDTO.Description, addedFoodProduct.Description);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_SaveImageTemporarilyTempPathEmpty(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();

            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageHandler>()
                    .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                    .Throws(new Exception(MessageContainer.EmptyPath));

                var foodProductProcessor = mock.Create<FoodProductProcessor>();

                // Act
                Action actionCreateFoodProduct = () => foodProductProcessor.CreateFoodProduct(foodProductDTO);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionCreateFoodProduct);
                Assert.NotNull(ex);
                Assert.Equal(MessageContainer.EmptyPath, ex.Message);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_SaveImageTemporarilyDirectoryNotExist(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();

            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageHandler>()
                    .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                    .Throws(new Exception(MessageContainer.DirectoryNotExist));

                var foodProductProcessor = mock.Create<FoodProductProcessor>();

                // Act
                Action actionCreateFoodProduct = () => foodProductProcessor.CreateFoodProduct(foodProductDTO);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionCreateFoodProduct);
                Assert.NotNull(ex);
                Assert.Equal(MessageContainer.DirectoryNotExist, ex.Message);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_CreateURIToSpecificPhoto_NullReferenceException(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();
            int mockID = 1;

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageHandler>()
                    .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}{Path.GetRandomFileName().Replace(".", "")}");

                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(mockID);

                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.CreateURIToSpecificPhoto(mockID))
                    .Throws(new NullReferenceException());

                var foodProductProcessor = mock.Create<FoodProductProcessor>();

                // Act
                Action actionCreateFoodProduct = () => foodProductProcessor.CreateFoodProduct(foodProductDTO);

                // Assert
                Exception ex = Assert.Throws<NullReferenceException>(actionCreateFoodProduct);
                Assert.NotNull(ex);
                Assert.IsType<NullReferenceException>(ex);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_CreateURIToSpecificPhoto_Exception(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();
            int mockID = 1;

            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IImageHandler>()
                    .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}{Path.GetRandomFileName().Replace(".", "")}");

                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(mockID);

                mock.Mock<IkFoodEngine>()
                    .Setup(x => x.CreateURIToSpecificPhoto(mockID))
                    .Throws(new Exception());

                var foodProductProcessor = mock.Create<FoodProductProcessor>();

                // Act
                Action actionCreateFoodProduct = () => foodProductProcessor.CreateFoodProduct(foodProductDTO);

                // Assert
                Exception ex = Assert.Throws<Exception>(actionCreateFoodProduct);
                Assert.NotNull(ex);
                Assert.IsType<Exception>(ex);
            }
        }

        [Theory]
        [MemberData(nameof(CreateFoodProductToPass))]
        public void CreateFoodProduct_NotCreated(FoodProductDTO foodProductDTO)
        {
            Mapper.AddProfile<MappingProfile>();

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IFoodProductsDAO>()
                    .Setup(x => x.CreateFoodProduct(It.IsAny<FoodProduct>()))
                    .Returns(0);

                mock.Mock<IImageHandler>()
                    .Setup(x => x.SaveImageTemporarily(foodProductDTO.FoodProductImage))
                    .Returns($"{ConfigurationManager.AppSettings["PathToTemporaryImage"]}{Path.GetRandomFileName().Replace(".", "")}");

                var cls = mock.Create<FoodProductProcessor>();

                // Act
                FoodProduct foodProduct = cls.CreateFoodProduct(foodProductDTO);

                // Assert
                Assert.Null(foodProduct);
            }
        }
        #endregion

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
                Description = "Sample description of food product",
                FoodImageURL = new Uri(string.Concat("http://localhost:51052/", ConfigurationManager.AppSettings["RouteToImage"], Convert.ToString(foodId)))
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
                new object[] { new FoodProductDTO()
                {
                    Name = "Food product test name 1",
                    Description = "Sample description of food product",
                    FoodProductImage = GetSampleImageAsBASE64() 
                } }
            };
        }

        /// <summary>
        /// Get sample image as BASE64 from configuration file
        /// </summary>
        /// <returns>The image as BASE64</returns>
        private static string GetSampleImageAsBASE64()
        {
            string testImage = ConfigurationManager.AppSettings["TestImage"];

            return File.ReadAllText(testImage);
        }
        #endregion
    }
}
