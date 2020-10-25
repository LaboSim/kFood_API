using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;

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

        /// <summary>
        /// The signature of method to create a new food product
        /// </summary>
        /// <param name="foodProductDTO">The instance of <see cref="FoodProductDTO"/></param>
        /// <param name="foodProduct">The instance of <see cref="FoodProduct"/> in case when create resource was successed</param>
        /// <returns>The flag indicating whether creating resource was succeeded</returns>
        bool CreateFoodProduct(FoodProductDTO foodProductDTO, out FoodProduct foodProduct);
    }
}
