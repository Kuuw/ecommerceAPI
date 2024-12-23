using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public static class MapperExtensions
    {
        public static void AddUserMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserDTO, User>()
               .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.Role, opt => opt.NullSubstitute("User"))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            cfg.CreateMap<User, UserDTO>()
               .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
        }

        public static void AddProductMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductDTO, Product>();
            cfg.CreateMap<Product, ProductDTO>();
        }

        public static void AddAddressMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AddressDTO, Address>();
            cfg.CreateMap<Address, AddressDTO>();
        }

        public static void AddCountryMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CountryDTO, Country>();
            cfg.CreateMap<Country, CountryDTO>();
        }

        public static void AddCartItemMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CartItem, CartItemDTO>();
            cfg.CreateMap<CartItemDTO, CartItem>();
        }

        public static void AddOrderMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderItemDTO, OrderItem>();
            cfg.CreateMap<OrderItem, OrderItemDTO>();
            cfg.CreateMap<OrderDTO, Order>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            cfg.CreateMap<Order, OrderDTO>();
        }

        public static void AddShipmentCompanyMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ShipmentCompanyDTO, ShipmentCompany>();
            cfg.CreateMap<ShipmentCompany, ShipmentCompanyDTO>();
        }

        public static void AddCategoryMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CategoryDTO, Category>();
            cfg.CreateMap<Category, CategoryDTO>();
        }

        public static void AddProductStockMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductStockDTO, ProductStock>();
            cfg.CreateMap<ProductStock, ProductStockDTO>();
        }

        public static void AddProductImageMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductImage, ProductImageDTO>();
            cfg.CreateMap<ProductImageDTO, ProductImage>();
        }
    }
}