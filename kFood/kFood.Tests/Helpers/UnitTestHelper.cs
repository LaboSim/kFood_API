using DataModelLibrary.Models.Foods;
using System;
using System.Collections.Generic;

namespace kFood.Tests.Helpers
{
    internal class UnitTestHelper
    {
        /// <summary>
        /// Get sample collection of foods
        /// </summary>
        /// <returns>The collection of <see cref="FoodProduct"/> instances</returns>
        internal static IList<FoodProduct> GetFoodsCollection()
        {
            IList<FoodProduct> outputFoods = new List<FoodProduct>();

            for (int i = 0; i < 5; i++)
            {
                FoodProduct foodProduct = new FoodProduct()
                {
                    Id = i,
                    Name = $"Food Name No.{i}",
                    Description = $"Food Description No.{i}",
                    FoodImageURL = new Uri($"http://localhost:51052/getFood/{i}")
                };

                outputFoods.Add(foodProduct);
            }

            return outputFoods;
        }
    }
}
