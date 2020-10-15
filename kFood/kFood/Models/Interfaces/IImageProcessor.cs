using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kFood.Models.Interfaces
{
    public interface IImageProcessor
    {
        byte[] GetMainImageForSpecificFoodProduct(int foodId);
    }
}
