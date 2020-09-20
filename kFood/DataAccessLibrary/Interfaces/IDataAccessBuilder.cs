namespace DataAccessLibrary.Interfaces
{
    /// <summary>
    /// The interface of data access builder
    /// </summary>
    public interface IDataAccessBuilder
    {
        /// <summary>
        /// Establish connection string based on web.Config file
        /// </summary>
        /// <returns>The connection string</returns>
        string EstablishConnectionString();
    }
}
