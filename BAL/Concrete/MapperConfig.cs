using AutoMapper;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Abstract;

namespace BAL.Concrete
{
    public class MapperConfig: IMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegister, User>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
