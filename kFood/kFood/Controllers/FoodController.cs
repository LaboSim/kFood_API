using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public FoodController()
        {
            _foodProductsDAO = new FoodProductsDAO();
        }

        /// <summary>
        /// Get food product with details
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns></returns>
        public IHttpActionResult GetFood(int id)
        {
            FoodProduct foodProduct;

            foodProduct = _foodProductsDAO.GetFoodProduct(id);
            if (foodProduct != null)
                return Ok(foodProduct);

            return NotFound();
        }
    }
}
