using DataModelLibrary.Models.Foods;

namespace DataAccessLibrary.Interfaces
{
    /// <summary>
    /// The interface of food product data access
    /// </summary>
    public interface IFoodProductsDAO
    {
        /// <summary>
        /// Get specific food product
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The instance of <see cref="FoodProduct"/></returns>
        FoodProduct GetFoodProduct(int foodId);
    }
}
