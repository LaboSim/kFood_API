using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLibrary.Models.Foods
{
    /// <summary>
    /// The base model of food product
    /// </summary>
    public class FoodProduct
    {
        /// <summary>
        /// The food product identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of food product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The photo of food in Base64
        /// </summary>
        public string FoodPhoto { get; set; }
    }
}
