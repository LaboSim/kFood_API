using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using System;
using System.Configuration;

namespace BusinessLogicLibrary.ConfigurationEngine
{
    /// <summary>
    /// The kFoodEngine to handle app configuration
    /// </summary>
    public class kFoodEngine : IkFoodEngine
    {
        /// <summary>
        /// Get the path from app configuration to save a temporary file 
        /// </summary>
        /// <returns>The path to save a temporary image</returns>
        /// <exception cref="NullReferenceException">Returned when cannot read web.Config for some reason</exception>
        /// <exception cref="Exception">Returned when occurs some other exception than <see cref="NullReferenceException"/></exception>
        public string GetTemporaryPath()
        {
            try
            {
                return ConfigurationManager.AppSettings["PathToTemporaryImage"];
            }
            catch(NullReferenceException ex)
            {
                throw new NullReferenceException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
