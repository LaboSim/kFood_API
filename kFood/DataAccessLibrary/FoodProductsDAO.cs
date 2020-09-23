using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using System;

namespace DataAccessLibrary
{
    public class FoodProductsDAO : IFoodProductsDAO
    {
        /// <summary>
        /// Get specific food product
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        public FoodProduct GetFoodProduct(int foodId)
        {
            throw new NotImplementedException();
        }
    }
}
