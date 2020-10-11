using DataAccessLibrary.Interfaces;
using DataModelLibrary.Models.Foods;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class FoodProductsDAO : DataAccessBuilder, IFoodProductsDAO
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public FoodProductsDAO() : base()
        {
        } 
        #endregion

        /// <summary>
        /// Get specific food product
        /// </summary>
        /// <param name="foodId">The food product identifier</param>
        /// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
        public FoodProduct GetFoodProduct(int foodId)
        {
            FoodProduct foodProduct;
            try
            {
                using (var con = new SqlConnection(EstablishConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetSpecificFoodProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@foodId", foodId));

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if(Convert.ToInt32(reader["Id"]) == foodId)
                        {
                            foodProduct = new FoodProduct()
                            {
                                Id = foodId,
                                Name = Convert.ToString(reader["Name"]),
                                FoodImageURL = new Uri(Convert.ToString(reader["FoodImageUrl"]))
                            };
                            return foodProduct;
                        } 
                    }
                    return (FoodProduct)null;
                }
            }
            catch (Exception e)
            {
                // TODO add logging + comment
                throw new Exception();
            }
        }
    }
}
