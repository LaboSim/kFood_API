using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using System.Web.Http;

namespace kFood.Controllers
{
    /// <summary>
    /// The main controller for food operation
    /// </summary>
    public class FoodController : ApiController
    {
        #region Private Members
        IFoodProductsDAO _foodProductsDAO;
        #endregion

        public FoodController(IFoodProductsDAO foodProductsDAO)
        {
            this._foodProductsDAO = foodProductsDAO;
        }

        /// <summary>
        /// Get food product with details
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns></returns>
        [Route("getFood/{id}")]
        public IHttpActionResult GetFood(int id)
        {
            FoodProduct foodProduct = _foodProductsDAO.GetFoodProduct(id);
            if (foodProduct != null)
                return Ok(foodProduct);

            return NotFound();
        }
    }
}
