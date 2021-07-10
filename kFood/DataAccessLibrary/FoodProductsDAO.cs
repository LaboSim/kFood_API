using DataAccessLibrary.Interfaces;
using DataModelLibrary.Messages;
using DataModelLibrary.Models.Foods;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DataAccessLibrary
{
	/// <summary>
	/// The food products data access object
	/// </summary>
	public class FoodProductsDAO : DataAccessBuilder, IFoodProductsDAO
	{
		#region Private Members
		private ILogger _logger; 
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public FoodProductsDAO() : base()
		{
			this._logger = Log.Logger.ForContext<FoodProductsDAO>();
		}
		#endregion

		/// <summary>
		/// Get specific food product from database
		/// </summary>
		/// <param name="foodId">The food product identifier</param>
		/// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
		public FoodProduct GetFoodProduct(int foodId)
		{
			_logger.Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

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
								Description = Convert.ToString(reader["Description"]),
								FoodImageURL = new Uri(Convert.ToString(reader["FoodImageUrl"]))
							};
							return foodProduct;
						} 
					}
					return (FoodProduct)null;
				}
			}
			catch(SqlException ex)
			{
				_logger.Error(MessageContainer.CaughtSQLException);
				throw ex;
			}
			catch (Exception ex)
			{
				_logger.Error(MessageContainer.CaughtException);
				throw ex;
			}
		}

        /// <summary>
        /// Get collection of foods from database
        /// </summary>
        /// <returns>The collection instances of <see cref="FoodProduct"/></returns>
        public IList<FoodProduct> GetFoods()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new food product
        /// </summary>
        /// <param name="foodProduct">The instance of <see cref="FoodProduct"/></param>
        /// <returns>The flag indicating whether a new food product was created</returns>
        public int CreateFoodProduct(FoodProduct foodProduct)
		{
			_logger.Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

			try
			{
				using(var con = new SqlConnection(EstablishConnectionString()))
				{
					SqlCommand cmd = new SqlCommand("sp_CreateFoodProduct", con);
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.Add(new SqlParameter("@Name", foodProduct.Name));
					cmd.Parameters.Add(new SqlParameter("@Description", foodProduct.Description));

					con.Open();
					int id = 0;
					bool created = int.TryParse(Convert.ToString(cmd.ExecuteScalar()), out id);
					if (created)
					{
						_logger.Information(MessageContainer.CreatedOnDatabase);
						foodProduct.Id = id;
						return foodProduct.Id;
					}
					else
						return 0;
				}
			}
			catch (SqlException ex)
			{
				_logger.Error(MessageContainer.CaughtSQLException);
				throw ex;
			}
			catch (Exception ex)
			{
				_logger.Error(MessageContainer.CaughtException);
				throw ex;
			}
		}

		public void UpdateUrlImage(int foodId, Uri photoUri)
		{

		}
    }
}
