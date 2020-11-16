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
    }
}
