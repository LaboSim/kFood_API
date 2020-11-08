using BusinessLogicLibrary.Converters.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace BusinessLogicLibrary.Converters
{
    /// <summary>
    /// The image converter
    /// </summary>
    public class ImageConverter : IImageConverter
    {
        /// <summary>
        /// Convert image as BASE64 string to image
        /// </summary>
        /// <param name="base64Image">The image as BASE64 string</param>
        /// <returns>The instance of <see cref="Image"/></returns>
        public Image ConvertToImage(string base64Image)
        {
            if (ValidateBase64(base64Image))
            {
                byte[] byteImage = Convert.FromBase64String(base64Image);

                Image image;
                using(MemoryStream ms = new MemoryStream(byteImage))
                {
                    image = Image.FromStream(ms);
                }

                return image;
            }
            else
            {
                return (Image)null;
            }
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
