using BusinessLogicLibrary.ConfigurationEngine;
using BusinessLogicLibrary.ConfigurationEngine.Interfaces;
using BusinessLogicLibrary.Images.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        #endregion

        /// <summary>
        /// Save an image temporarily
        /// </summary>
        /// <param name="base64Image">The image as BASE64 string</param>
        /// <returns>The filename</returns>
        public string SaveImageTemporarily(string base64Image)
        {
            if (ValidateBase64(base64Image))
            {
                byte[] byteImage = Convert.FromBase64String(base64Image);
                try
                {
                    _engine = _engine ?? new kFoodEngine();
                    string path = _engine.GetTemporaryPath();
                    if (string.IsNullOrEmpty(path))
                        throw new Exception(""); // TODO Error Message

                    Image image;
                    string pathImage = $"{path}{Path.GetRandomFileName().Replace(".", "")}.png";

                    using (MemoryStream ms = new MemoryStream(byteImage))
                    {
                        image = Image.FromStream(ms);
                        image.Save($"{pathImage}", ImageFormat.Png);
                    }

                    return pathImage;
                }
                catch(Exception ex)
                {
                    return string.Empty;
                }
            }

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
