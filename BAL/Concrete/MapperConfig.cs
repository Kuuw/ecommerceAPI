using AutoMapper;
using BAL.Abstract;

namespace BAL.Concrete
{
    public class MapperConfig : IMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddUserMappings();
                cfg.AddProductMappings();
                cfg.AddAddressMappings();
                cfg.AddCountryMappings();
                cfg.AddCartItemMappings();
                cfg.AddOrderMappings();
                cfg.AddShipmentCompanyMappings();
                cfg.AddCategoryMappings();
                cfg.AddProductStockMappings();
                cfg.AddProductImageMappings();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
