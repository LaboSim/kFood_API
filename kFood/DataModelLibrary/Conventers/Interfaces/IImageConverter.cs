using System.Drawing;

namespace DataModelLibrary.Conventers.Interfaces
{
    /// <summary>
    /// The interface of image converter
    /// </summary>
    public interface IImageConverter
    {
        /// <summary>
        /// The signature of method to convert image as BASE64 string to image
        /// </summary>
        /// <param name="base64Image">The image as BASE64 string</param>
        /// <returns>The instance of <see cref="Image"/></returns>
        Image ConvertToImage(string base64Image);
    }
}
