using System;

namespace BusinessLogicLibrary.ConfigurationEngine.Interfaces
{
    /// <summary>
    /// The interface of kFoodEngine application
    /// </summary>
    public interface IkFoodEngine
    {
        /// <summary>
        /// The signature of method to get the path from app configuration to save a temporary file
        /// </summary>
        /// <returns>The path to save a temporary image</returns>
        string GetTemporaryPath();

        /// <summary>
        /// The signature of method to create URI for photo of specific food product
        /// </summary>
        /// <param name="id">The identifier of food product</param>
        /// <returns>The instance <see cref="Uri"/> indicating for food product photo</returns>
        string CreateURIToSpecificPhoto(int id);
    }
}
