using AutoMapper;
using DataModelLibrary.DTO.Foods;
using DataModelLibrary.Models.Foods;

namespace kFood.App_Start
{
    /// <summary>
    /// The configuration AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public MappingProfile()
        {
            Mapper.CreateMap<FoodProductDTO, FoodProduct>();
        }
    }
}