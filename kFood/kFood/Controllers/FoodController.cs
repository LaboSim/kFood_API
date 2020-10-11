using DataAccessLibrary;
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

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public FoodController()
        {
        }

        /// <summary>
        /// The basic constructor to handle food products operation
        /// </summary>
        /// <param name="foodProductsDAO">The instance of data access to food products</param>
        public FoodController(IFoodProductsDAO foodProductsDAO) : this()
        {
            this._foodProductsDAO = foodProductsDAO;
        } 
        #endregion

        /// <summary>
        /// Get food product with details
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns>The information about specific food product</returns>
        [Route("getFood/{id}")]
        public IHttpActionResult GetFood(int id)
        {
            this._foodProductsDAO = this._foodProductsDAO ?? new FoodProductsDAO();
            FoodProduct foodProduct = _foodProductsDAO.GetFoodProduct(id);
            if (foodProduct != null)
                return Ok(foodProduct);

            return NotFound();
        }
    }
}
