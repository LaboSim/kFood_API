namespace DataModelLibrary.DTO.Foods
{
    public class FoodProductDTO
    {
        /// <summary>
        /// The name of food product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of food product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The food product image as BASE64
        /// </summary>
        public string FoodProductImage { get; set; }
    }
}
