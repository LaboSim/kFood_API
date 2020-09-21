using Autofac.Extras.Moq;
using DataAccessLibrary;
using DataAccessLibrary.Interfaces;
using System.Configuration;
using Xunit;

namespace kFood.Tests.DAO
{
    /// <summary>
    /// The class for testing data access builder
    /// </summary>
    public class DataAccessBuilderTests
    {
        [Fact]
        public void GetConnectionStringFromConfigFile_Success()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDataAccessBuilder>()
                    .Setup(x => x.EstablishConnectionString())
                    .Returns(ConfigurationManager.ConnectionStrings["kFood_DEV"].ConnectionString);

                var cls = mock.Create<DataAccessBuilder>();
                string expectedMagicConnectionString = @"Server=SZYMON\BASE;Database=kFood_DEV;Integrated Security=SSPI;";
                var actualConnectionString = cls.EstablishConnectionString();

                Assert.Equal(expectedMagicConnectionString, actualConnectionString);
            }
        }
    }
}
