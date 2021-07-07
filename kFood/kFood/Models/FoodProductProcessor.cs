using AutoMapper;
using BusinessLogicLibrary.ConfigurationEngine;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
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
        private IImageHandler _imageHandler;
        private IkFoodEngine _kFoodEngine;
        private ILogger _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public FoodProductProcessor()
        {
            this._logger = Log.Logger.ForContext<FoodProductProcessor>();
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="foodProductsDAO">The injected instance of <see cref="IFoodProductsDAO"/> to unit tests</param>
        /// <param name="imageHandler">The injected instance of <see cref="IImageHandler"/> to unit tests</param>
        /// <param name="kFoodEngine">The injected instance of <see cref="IkFoodEngine"/> to unit tests</param>
        public FoodProductProcessor(IFoodProductsDAO foodProductsDAO, IImageHandler imageHandler, IkFoodEngine kFoodEngine)
        {
            this._foodProductsDAO = foodProductsDAO;
            this._imageHandler = imageHandler;
            this._kFoodEngine = kFoodEngine;
            this._logger = Log.Logger.ForContext<FoodProductProcessor>();
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
            _logger.Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

            FoodProduct foodProduct = Mapper.Map<FoodProduct>(foodProductDTO);

            /*
             * Convert base 64 to byte[] + SaveImageTemporarily
             * Put photo to folder + SaveImageTemporarily
             * Save food product in database +
             * Create URI to photo +
             * Update food product with URI photo value
             * Save image of food product in database
             * Remove temp image
            */
            try
            {
                // Convert base 64 to byte[] & Put photo to folder
                _imageHandler = _imageHandler ?? new ImageHandler();
                string tempFilename = _imageHandler.SaveImageTemporarily(foodProductDTO.FoodProductImage);

                // Save food product in database
                _foodProductsDAO = _foodProductsDAO ?? new FoodProductsDAO();
                int foodProductID = _foodProductsDAO.CreateFoodProduct(foodProduct);
                if(foodProductID == 0)
                    return (FoodProduct)null;

                // Create URI to photo
                _kFoodEngine = _kFoodEngine ?? new kFoodEngine();
                string photoURI = _kFoodEngine.CreateURIToSpecificPhoto(foodProductID);
                foodProduct.FoodImageURL = new Uri(photoURI);

                // Update food product with URI photo value
                _foodProductsDAO = _foodProductsDAO ?? new FoodProductsDAO(); // -> to test and implement
                _foodProductsDAO.UpdateUrlImage(foodProduct.Id, foodProduct.FoodImageURL);

                bool created = true;

                if (created)
                    return foodProduct;
                else
                    return (FoodProduct)null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}