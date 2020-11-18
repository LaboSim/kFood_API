namespace BusinessLogicLibrary.Images.Interfaces
{
    /// <summary>
    /// The interface of image handler
    /// </summary>
    public interface IImageHandler
    {
        /// <summary>
        /// The signature of method to save an image temporarily
        /// </summary>
        /// <param name="base64Image">The image as BASE64 string</param>
        /// <returns>The filename</returns>
        string SaveImageTemporarily(string base64Image);
    }
}
