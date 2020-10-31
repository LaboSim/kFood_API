using DataModelLibrary.Models.Foods;

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
        /// The signature of method to create a new food product
        /// </summary>
        /// <param name="foodProduct">The instance of <see cref="FoodProduct"/></param>
        /// <returns>The flag indicating whether a new food product was created</returns>
        bool CreateFoodProduct(FoodProduct foodProduct);
    }
}
