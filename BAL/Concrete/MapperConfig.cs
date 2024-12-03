using AutoMapper;
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
                   .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
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
                cfg.CreateMap<OrderDTO, Order>();
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<ShipmentCompanyDTO, ShipmentCompany>();
                cfg.CreateMap<ShipmentCompany, ShipmentCompanyDTO>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
