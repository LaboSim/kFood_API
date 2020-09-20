using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace kFood.Tests.DAO
{
    public class DataAccessBuilderTests
    {
        [Fact]
        public void GetConnectionStringFromConfigFile_Success()
        {
            // Arange
            string expectedMagicConnectionString = @"Server=SZYMON\BASE;Database=kFood_DEV;Integrated Security=SSPI";
            IDataAccessBuilder dataAccessBuilder = new DataAccessBuilder();

            // Act
            string actualConnectionString = dataAccessBuilder.EstablishConnectionString();

            // Assert
            Assert.Equal(expectedMagicConnectionString, actualConnectionString);
        }
    }
}
