﻿using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using kFood.Models.Interfaces;

namespace kFood.Models
{
    /// <summary>
    /// The processor layer of food product
    /// </summary>
    public class FoodProductProcessor : IFoodProductProcessor
    {
        #region Private Members
        private IFoodProductsDAO _foodProductsDAO;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public FoodProductProcessor()
        {
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="foodProductsDAO">The injected instance of <see cref="IFoodProductsDAO"/> to unit tests</param>
        public FoodProductProcessor(IFoodProductsDAO foodProductsDAO)
        {
            this._foodProductsDAO = foodProductsDAO;
        } 
        #endregion

        /// <summary>
        /// Get specific food product
        /// </summary>
        /// <param name="foodId">The identifier of food product</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        public FoodProduct GetSpecificFoodProduct(int foodId)
        {
            _foodProductsDAO = _foodProductsDAO ?? new FoodProductsDAO();
            return _foodProductsDAO.GetFoodProduct(foodId);
        }
    }
}