using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class MapperConfig : IMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                   .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                   .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                   .ForMember(dest => dest.Role, opt => opt.NullSubstitute("User"))
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                cfg.CreateMap<User, UserDTO>()
                   .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<AddressDTO, Address>();
                cfg.CreateMap<Address, AddressDTO>();
                cfg.CreateMap<CountryDTO, Country>();
                cfg.CreateMap<Country, CountryDTO>();
                cfg.CreateMap<CartItem, CartItemDTO>();
                cfg.CreateMap<CartItemDTO, CartItem>();
                cfg.CreateMap<OrderItemDTO, OrderItem>();
                cfg.CreateMap<OrderItem, OrderItemDTO>();
                cfg.CreateMap<OrderDTO, Order>()
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<ShipmentCompanyDTO, ShipmentCompany>();
                cfg.CreateMap<ShipmentCompany, ShipmentCompanyDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<ProductStockDTO, ProductStock>();
                cfg.CreateMap<ProductStock, ProductStockDTO>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
