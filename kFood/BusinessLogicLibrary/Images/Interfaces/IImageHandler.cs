using System.Drawing;

namespace BusinessLogicLibrary.Images.Interfaces
{
    /// <summary>
    /// The interface of image handler
    /// </summary>
    public interface IImageHandler
    {
        /// <summary>
        /// The signature of method to place temporary image in folder
        /// </summary>
        /// <param name="image">The instance of temporary <see cref="Image"/></param>
        /// <returns>The filename</returns>
        string PlaceImageInFolder(Image image);
    }
}
