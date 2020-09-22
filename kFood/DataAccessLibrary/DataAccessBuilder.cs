using DataAccessLibrary.Interfaces;
using System;
using System.Configuration;

namespace DataAccessLibrary
{
    /// <summary>
    /// Establish connection string based on web.Config file
    /// </summary>
    public class DataAccessBuilder : IDataAccessBuilder
    {
        /// <summary>
        /// Establish connection string by getting it from configuration file
        /// </summary>
        /// <returns>The connection string</returns>
        /// <exception cref="NullReferenceException">Returned when cannot read web.Config for some reason</exception>
        /// <exception cref="Exception">Returned when occurs some other exception than <see cref="NullReferenceException"/></exception>
        public string EstablishConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["kFood_DEV"].ConnectionString;
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
    }
}
