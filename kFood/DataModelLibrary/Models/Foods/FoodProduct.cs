using System;

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
        /// The description of food product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The path to image
        /// </summary>
        public Uri FoodImageURL { get; set; }
    }
}
