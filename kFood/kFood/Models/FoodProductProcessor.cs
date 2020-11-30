﻿using AutoMapper;
using BusinessLogicLibrary.Images;
using BusinessLogicLibrary.Images.Interfaces;
using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Messages;
using DataModelLibrary.Models.Foods;
using kFood.Models.Interfaces;
using Serilog;
using System;
using System.Reflection;

namespace kFood.Models
{
    /// <summary>
    /// The processor layer of food product
    /// </summary>
    public class FoodProductProcessor : IFoodProductProcessor
    {
        #region Private Members
        private IFoodProductsDAO _foodProductsDAO;
        private ILogger _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="logger">The instance of <see cref="ILogger"/></param>
        public FoodProductProcessor(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="foodProductsDAO">The injected instance of <see cref="IFoodProductsDAO"/> to unit tests</param>
        /// <param name="logger">The instance of <see cref="ILogger"/></param>
        public FoodProductProcessor(IFoodProductsDAO foodProductsDAO, ILogger logger)
        {
            this._foodProductsDAO = foodProductsDAO;
            this._logger = logger;
        }
        #endregion

        /// <summary>
        /// Get specific food product
        /// </summary>
        /// <param name="foodId">The identifier of food product</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        public FoodProduct GetSpecificFoodProduct(int foodId)
        {
            _logger.Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

            try
            {
                _foodProductsDAO = _foodProductsDAO ?? new FoodProductsDAO();
                return _foodProductsDAO.GetFoodProduct(foodId);
            }
            catch(Exception ex)
            {
                _logger.Error(MessageContainer.CaughtException);
                throw ex;
            }
        }

        /// <summary>
        /// Create a new food product
        /// </summary>
        /// <param name="foodProductDTO">The instance of <see cref="FoodProductDTO"/></param>
        /// <returns>The instance of <see cref="FoodProduct"/> if adding resource was succeeded</returns>
        public FoodProduct CreateFoodProduct(FoodProductDTO foodProductDTO)
        {
            FoodProduct foodProduct = Mapper.Map<FoodProduct>(foodProductDTO);

            /*
             * Convert base 64 to byte[] + 
             * Put photo to foler +
             * Create URI to photo
             * Save in database
             * Remove temp image
            */

            IImageHandler imageHandler = new ImageHandler();
            string tempFilename = imageHandler.SaveImageTemporarily(foodProductDTO.FoodProductImage);

            _foodProductsDAO = _foodProductsDAO ?? new FoodProductsDAO();
            bool created = _foodProductsDAO.CreateFoodProduct(foodProduct);

            if (created)
                return foodProduct;
            else
                return (FoodProduct)null;
        }
    }
}