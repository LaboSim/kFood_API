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
        /// <summary>
        /// Get food product with details
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns></returns>
        public IHttpActionResult GetFood(int id)
        {
            throw new NotImplementedException();
        }
    }
}
