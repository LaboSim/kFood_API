using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace kFood.Tests.Controllers
{
    public class ImageControllerTests
    {
        [Theory]
        [InlineData(4)]
        public void GetMainImageForFoodProduct_Successful(int foodId)
        {
            using(var mock = AutoMock.GetLoose())
            {

            }
        }
    }
}
