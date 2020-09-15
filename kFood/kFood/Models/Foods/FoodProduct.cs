using kFood.ResourcesManager;
using System.ComponentModel.DataAnnotations;

namespace kFood.Models.Foods
{
    /// <summary>
    /// The base model of food product
    /// </summary>
    public class FoodProduct
    {
        /// <summary>
        /// The name of food product
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.FoodProductRequireError))]
        [StringLength(255, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.FoodProductNameError))]
        public string Name { get; set; }

        /// <summary>
        /// The photo of food in Base64
        /// </summary>
        public string FoodPhoto { get; set; }
    }
}