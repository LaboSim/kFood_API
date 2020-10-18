namespace DataAccessLibrary.Interfaces
{
    /// <summary>
    /// The interface of image data access
    /// </summary>
    public interface IImageDAO
    {
        /// <summary>
        /// The signature of method to get specific food product main image from database
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The image as byte[]</returns>
        byte[] GetFoodProductMainImage(int foodId);
    }
}
