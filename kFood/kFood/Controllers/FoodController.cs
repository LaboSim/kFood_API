using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;
using kFood.Models;
using kFood.Models.Interfaces;
using System;
using System.Web.Http;

namespace kFood.Controllers
{
    /// <summary>
    /// The main controller for food operation
    /// </summary>
    public class FoodController : ApiController
    {
        #region Private Members
        private IFoodProductProcessor _foodProductProcessor;
        #endregion


        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public FoodController()
        {
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="foodProductProcessor">The injected instance of <see cref="IFoodProductProcessor"/> to unit tests.</param>
        public FoodController(IFoodProductProcessor foodProductProcessor) 
        {
            this._foodProductProcessor = foodProductProcessor;
        } 
        #endregion

        /// <summary>
        /// Get food product with details
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns>The information about specific food product</returns>
        [HttpGet]
        [Route("getFood/{id}")]
        public IHttpActionResult GetFood(int id)
        {
            _foodProductProcessor = _foodProductProcessor ?? new FoodProductProcessor();
            FoodProduct foodProduct = _foodProductProcessor.GetSpecificFoodProduct(id);

            if (foodProduct != null)
                return Ok(foodProduct);

            return NotFound();
        }

        /// <summary>
        /// Create a new food product
        /// </summary>
        /// <param name="foodProductDTO">The instance of <see cref="FoodProductDTO"/> in POST request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createFoodProduct")]
        public IHttpActionResult CreateFoodProduct(FoodProductDTO foodProductDTO)
        {
            if(foodProductDTO == null)
                return BadRequest();

            _foodProductProcessor = _foodProductProcessor ?? new FoodProductProcessor();

            FoodProduct foodProduct = _foodProductProcessor.CreateFoodProduct(foodProductDTO);
            if (foodProduct != null)
                return Created<FoodProduct>(foodProduct.FoodImageURL, foodProduct);

            return Conflict();
        }
    }
}
