using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using System;
using System.Configuration;
using System.Web;

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

        /// <summary>
        /// Create URI for photo of specific food product
        /// </summary>
        /// <param name="id">The identifier of food product</param>
        /// <returns>The instance <see cref="Uri"/> indicating for food product photo</returns>
        public string CreateURIToSpecificPhoto(int id)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var appUrl = HttpRuntime.AppDomainAppVirtualPath;
                if (appUrl != "/")
                    appUrl = "/" + appUrl;

                var actualAppUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
                var partialRouteToImage = ConfigurationManager.AppSettings["RouteToImage"];

                var baseUrl = string.Concat(actualAppUrl, partialRouteToImage);
                return baseUrl;
            }
            catch(NullReferenceException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
