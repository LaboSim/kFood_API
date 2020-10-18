using DataAccessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    /// <summary>
    /// The image data access object
    /// </summary>
    public class ImageDAO : DataAccessBuilder, IImageDAO
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public ImageDAO() : base()
        {
        }
        #endregion

        /// <summary>
        /// Get specific food product main image from database
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The image as byte[]</returns>
        public byte[] GetFoodProductMainImage(int foodId)
        {
            try
            {
                byte[] image = new byte[] { };
                using (var con = new SqlConnection(EstablishConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetFoodProductMainImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@foodId", foodId));

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        image = (byte[])reader["Image"];
                    }

                    // TODO: add exception if empty image

                    return image;
                }
            }
            catch(Exception e)
            {
                // TODO add logging + comment
                throw new Exception();
            }
        }
    }
}
