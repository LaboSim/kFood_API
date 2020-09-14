namespace kFood.Models.Foods
{
    /// <summary>
    /// The base model of food product
    /// </summary>
    public class FoodProduct
    {
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