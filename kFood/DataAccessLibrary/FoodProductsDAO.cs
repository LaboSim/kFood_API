using DataAccessLibrary.Interfaces;
using DataModelLibrary.Messages;
using DataModelLibrary.Models.Foods;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DataAccessLibrary
{
	/// <summary>
	/// 
	/// </summary>
	public class FoodProductsDAO : DataAccessBuilder, IFoodProductsDAO
	{
		private ILogger _logger;

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public FoodProductsDAO(ILogger logger) : base()
		{
			this._logger = logger;
		}
		#endregion

		/// <summary>
		/// Get specific food product from database
		/// </summary>
		/// <param name="foodId">The food product identifier</param>
		/// <returns>The instance of <see cref="FoodProduct"/> if exist</returns>
		public FoodProduct GetFoodProduct(int foodId)
		{
			_logger.ForContext<FoodProductsDAO>().Information(MessageContainer.CalledMethod, MethodBase.GetCurrentMethod().Name);

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
			catch(SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				// TODO add logging + comment
				throw ex;
			}
		}

		/// <summary>
		/// Create a new food product
		/// </summary>
		/// <param name="foodProduct">The instance of <see cref="FoodProduct"/></param>
		/// <returns>The flag indicating whether a new food product was created</returns>
		public bool CreateFoodProduct(FoodProduct foodProduct)
		{
			// TODO: Delete sp_CreateFoodProduct

			string query = @"INSERT INTO FoodProducts (Name) VALUES (@Name)
							INSERT INTO FoodImageStore (Image, Description, FoodId, MainImage) 
							VALUES ((SELECT * FROM OPENROWSET(BULK 'D:\Szymon\Temporary\Lasagne.jpg', SINGLE_BLOB)Image), @Description, SCOPE_IDENTITY(), @MainImage)";

			try
			{
				using(var con = new SqlConnection(EstablishConnectionString()))
				{
					SqlCommand cmd = new SqlCommand(query, con);
					cmd.CommandType = CommandType.Text;

					cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar).Value = foodProduct.Name);
					cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar).Value = foodProduct.Description);
				}

				// only test
				return true;
			}
			catch (Exception e)
			{
				// only test
				return false;
			}
		}
	}
}
