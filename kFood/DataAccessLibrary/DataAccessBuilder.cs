using DataAccessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    /// <summary>
    /// Establish connection string based on web.Config file
    /// </summary>
    public class DataAccessBuilder : IDataAccessBuilder
    {
        public string EstablishConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
