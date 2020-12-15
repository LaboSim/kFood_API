using BusinessLogicLibrary.ConfigurationEngine;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using BusinessLogicLibrary.Images.Interfaces;
using DataModelLibrary.Messages;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BusinessLogicLibrary.Images
{
    /// <summary>
    /// Place temporary image in folder
    /// </summary>
    public class ImageHandler : IImageHandler
    {
        #region Private Members
        private IkFoodEngine _engine;
        private ILogger _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageHandler()
        {
            this._logger = Log.Logger.ForContext<ImageHandler>();
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="kFoodEngine">The injected instance of <see cref="IkFoodEngine"/> to unit tests</param>
        public ImageHandler(IkFoodEngine kFoodEngine)
        {
            this._engine = kFoodEngine;
            this._logger = Log.Logger.ForContext<ImageHandler>();
        }
        #endregion

        /// <summary>
        /// Save an image temporarily
        /// </summary>
        /// <param name="base64Image">The image as BASE64 string</param>
        /// <returns>The filename</returns>
        public string SaveImageTemporarily(string base64Image)
        {
            _logger.Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

            if (ValidateBase64(base64Image))
            {
                _logger.Information(MessageContainer.Base64Valid);

                byte[] byteImage = Convert.FromBase64String(base64Image);
                try
                {
                    _engine = _engine ?? new kFoodEngine();
                    string path = _engine.GetTemporaryPath();
                    if (string.IsNullOrEmpty(path))
                    {
                        _logger.Fatal(MessageContainer.EmptyPath);
                        throw new Exception(MessageContainer.EmptyPath); 
                    }

                    if (!Directory.Exists(path))
                    {
                        _logger.Fatal(MessageContainer.DirectoryNotExist);
                        throw new Exception(MessageContainer.DirectoryNotExist);
                    }
                        
                    Image image;
                    string pathImage = $"{path}{Path.GetRandomFileName().Replace(".", "")}.png";

                    using (MemoryStream ms = new MemoryStream(byteImage))
                    {
                        image = Image.FromStream(ms);
                        image.Save($"{pathImage}", ImageFormat.Png);
                    }

                    _logger.Information(MessageContainer.TempImagePath, pathImage);
                    return pathImage;
                }
                catch(Exception ex)
                {
                    _logger.Error(MessageContainer.CaughtException);
                    throw ex;
                }
            }

            _logger.Warning(MessageContainer.Base64Invalid);
            return string.Empty;
        }

        /// <summary>
        /// Validate whether input is BASE64 string
        /// </summary>
        /// <param name="base64Image">The BASE64 string</param>
        /// <returns>True whether string is BASE64</returns>
        private bool ValidateBase64(string base64Image)
        {
            string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(base64Image);
        }
    }
}
