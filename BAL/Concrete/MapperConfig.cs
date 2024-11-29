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
                   .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)); ;
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<AddressDTO, Address>();
                cfg.CreateMap<Address, AddressDTO>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
