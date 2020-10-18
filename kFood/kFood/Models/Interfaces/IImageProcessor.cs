namespace kFood.Models.Interfaces
{
    /// <summary>
    /// The interface of processor layer of image
    /// </summary>
    public interface IImageProcessor
    {
        /// <summary>
        /// The signature of method to get food product main image
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The image as byte[]</returns>
        byte[] GetMainImageForSpecificFoodProduct(int foodId);
    }
}
