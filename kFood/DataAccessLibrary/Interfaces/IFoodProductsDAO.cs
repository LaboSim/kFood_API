using DataModelLibrary.Models.Foods;
using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Interfaces
{
    /// <summary>
    /// The interface of food product data access
    /// </summary>
    public interface IFoodProductsDAO
    {
        /// <summary>
        /// The signature of method to get specific food product from database
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The instance of <see cref="FoodProduct"/></returns>
        FoodProduct GetFoodProduct(int foodId);

        /// <summary>
        /// The signature of method which get collection of foods from database
        /// </summary>
        /// <returns>The collection instances of <see cref="FoodProduct"/></returns>
        IList<FoodProduct> GetFoods();

        /// <summary>
        /// The signature of method to create a new food product
        /// </summary>
        /// <param name="foodProduct">The instance of <see cref="FoodProduct"/></param>
        /// <returns>The identifier of created food product</returns>
        int CreateFoodProduct(FoodProduct foodProduct);

        /// <summary>
        /// The signature of method to update food product image Url
        /// </summary>
        /// <param name="foodId">The identifier of food product to update photo Uri</param>
        /// <param name="photoUri">The instance of <see cref="Uri"/> indicating on food product main photo</param>
        void UpdateUrlImage(int foodId, Uri photoUri);
    }
}
