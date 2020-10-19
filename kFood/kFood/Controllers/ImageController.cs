using kFood.Models;
using kFood.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace kFood.Controllers
{
    /// <summary>
    /// Handle operation on images
    /// </summary>
    [RoutePrefix("view")]
    public class ImageController : ApiController
    {
        #region Private Members
        IImageProcessor _imageProcessor;
        #endregion

        #region Constructos
        /// <summary>
        /// The default constructor
        /// </summary>
        public ImageController()
        {
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="imageProcessor">The injected instance of <see cref="IImageProcessor"/> to unit tests</param>
        public ImageController(IImageProcessor imageProcessor)
        {
            this._imageProcessor = imageProcessor;
        } 
        #endregion

        /// <summary>
        /// Get food product main image
        /// </summary>
        /// <param name="id">The food product identifier</param>
        /// <returns>The <see cref="ByteArrayContent"/> image</returns>
        [HttpGet]
        [Route("getImage/{id}")]
        public IHttpActionResult GetFoodProductMainImage(int id)
        {
            _imageProcessor = _imageProcessor ?? new ImageProcessor();
            byte[] image = _imageProcessor.GetMainImageForSpecificFoodProduct(id);

            IHttpActionResult response;
            HttpResponseMessage responseMessage;
            if (image != null && image.Length > 0)
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.Content = new ByteArrayContent(_imageProcessor.GetMainImageForSpecificFoodProduct(id));
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }
            else
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            response = ResponseMessage(responseMessage);
            return response;
        }
    }
}
