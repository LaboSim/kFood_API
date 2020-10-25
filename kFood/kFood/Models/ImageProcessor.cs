﻿using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using kFood.Models.Interfaces;

namespace kFood.Models
{
    /// <summary>
    /// The processor layer of image
    /// </summary>
    public class ImageProcessor : IImageProcessor
    {
        #region Private Members
        IImageDAO _imageDAO;
        #endregion

        #region Constructors
        /// <summary>
        /// Dedault constructor
        /// </summary>
        public ImageProcessor()
        {
        }

        /// <summary>
        /// The parameterized constructor for unit tests
        /// </summary>
        /// <param name="imageDAO">The injected instance of <see cref="IImageDAO"/> to unit tests</param>
        public ImageProcessor(IImageDAO imageDAO)
        {
            this._imageDAO = imageDAO;
        }
        #endregion

        /// <summary>
        /// Get food product main image
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The image as byte[]</returns>
        public byte[] GetMainImageForSpecificFoodProduct(int foodId)
        {
            _imageDAO = _imageDAO ?? new ImageDAO();
            return _imageDAO.GetFoodProductMainImage(foodId);
        }
    }
}