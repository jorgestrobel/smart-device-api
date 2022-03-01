using AutoMapper;
using SmartDevice.Dto;
using SmartDevice.Models;

namespace SmartDevice.Data
{
    public class SmartDeviceProfile: Profile
    {
        public SmartDeviceProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();

            CreateMap<Category, CategoryDto>();

            CreateMap<User, UserDto>();
        }

    }
}
