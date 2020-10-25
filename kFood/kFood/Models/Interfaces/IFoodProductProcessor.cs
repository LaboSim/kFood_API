﻿using DataModelLibrary.Models.Foods;

namespace kFood.Models.Interfaces
{
    /// <summary>
    /// The interface of processor layer of food product
    /// </summary>
    public interface IFoodProductProcessor
    {
        /// <summary>
        /// The signature of method to get specific food product
        /// </summary>
        /// <param name="foodId">The identifier of food product</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        FoodProduct GetSpecificFoodProduct(int foodId);
    }
}