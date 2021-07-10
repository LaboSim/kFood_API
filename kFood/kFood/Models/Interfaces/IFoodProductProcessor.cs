using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;
using System.Collections.Generic;

namespace kFood.Models.Interfaces
{
    /// <summary>
    /// The interface of processor layer of food product
    /// </summary>
    public interface IFoodProductProcessor
    {
        /// <summary>
        /// The signature of method which get specific food product
        /// </summary>
        /// <param name="foodId">The identifier of food product</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        FoodProduct GetSpecificFoodProduct(int foodId);

        /// <summary>
        /// The signature of method which get collection of foods
        /// </summary>
        /// <returns>The collection instances of <see cref="FoodProduct"/></returns>
        IEnumerable<FoodProduct> GetFoods();

        /// <summary>
        /// The signature of method which create a new food product
        /// </summary>
        /// <param name="foodProductDTO">The instance of <see cref="FoodProductDTO"/></param>
        /// <returns>The instance of <see cref="FoodProduct"/> if adding resource was succeeded</returns>
        FoodProduct CreateFoodProduct(FoodProductDTO foodProductDTO);
    }
}
