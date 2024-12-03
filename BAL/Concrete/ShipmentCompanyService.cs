using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class ShipmentCompanyService : IShipmentCompanyService
    {
        private readonly IShipmentCompanyRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public ShipmentCompanyService(IShipmentCompanyRepository repository)
        {
            _repository = repository;
        }

        public ShipmentCompanyDTO Add(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            var shipmentCompany = mapper.Map<ShipmentCompany>(shipmentCompanyDTO);
            _repository.Insert(shipmentCompany);
            shipmentCompanyDTO.ShipmentCompanyId = shipmentCompany.ShipmentCompanyId;
            return shipmentCompanyDTO;
        }

        public void Delete(int id)
        {
            var shipmentCompany = _repository.GetById(id);
            if (shipmentCompany != null)
            {
                _repository.Delete(shipmentCompany);
            }
        }

        public List<ShipmentCompanyDTO> Get()
        {
            var listDTO = new List<ShipmentCompanyDTO>();
            var list = _repository.List();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(mapper.Map<ShipmentCompanyDTO>(list[i]));
            }
            return listDTO;
        }

        public ShipmentCompanyDTO? GetById(int id)
        {
            var shipmentCompany = _repository.GetById(id);
            if (shipmentCompany != null) { return mapper.Map<ShipmentCompanyDTO>(shipmentCompany); }
            else { return null; }
        }

        public void Update(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            if (shipmentCompanyDTO.ShipmentCompanyId == null) { return; }
            var shipmentCompany = _repository.GetById((int)shipmentCompanyDTO.ShipmentCompanyId);

            if (shipmentCompany == null) { return; }

            mapper.Map(shipmentCompanyDTO, shipmentCompany);
            _repository.Update(shipmentCompany);
        }
    }
}
