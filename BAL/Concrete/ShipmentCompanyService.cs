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

        public ServiceResult<ShipmentCompanyDTO> Add(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            var shipmentCompany = mapper.Map<ShipmentCompany>(shipmentCompanyDTO);
            _repository.Insert(shipmentCompany);
            shipmentCompanyDTO.ShipmentCompanyId = shipmentCompany.ShipmentCompanyId;
            return ServiceResult<ShipmentCompanyDTO>.Ok(shipmentCompanyDTO);
        }

        public ServiceResult<bool> Delete(int id)
        {
            var shipmentCompany = _repository.GetById(id);
            if (shipmentCompany != null)
            {
                _repository.Delete(shipmentCompany);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.NotFound("Shipment company not found.");
        }

        public ServiceResult<List<ShipmentCompanyDTO>> Get()
        {
            var listDTO = new List<ShipmentCompanyDTO>();
            var list = _repository.List();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(mapper.Map<ShipmentCompanyDTO>(list[i]));
            }
            return ServiceResult<List<ShipmentCompanyDTO>>.Ok(listDTO);
        }

        public ServiceResult<ShipmentCompanyDTO?> GetById(int id)
        {
            var shipmentCompany = _repository.GetById(id);
            if (shipmentCompany != null)
            {
                var shipmentCompanyDto = mapper.Map<ShipmentCompanyDTO>(shipmentCompany);
                return ServiceResult<ShipmentCompanyDTO?>.Ok(shipmentCompanyDto);
            }
            else
            {
                return ServiceResult<ShipmentCompanyDTO?>.NotFound("Shipment company not found.");
            }
        }

        public ServiceResult<bool> Update(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            if (shipmentCompanyDTO.ShipmentCompanyId == null) { return ServiceResult<bool>.BadRequest("ShipmentCompanyId cannot be null."); }
            var shipmentCompany = _repository.GetById((int)shipmentCompanyDTO.ShipmentCompanyId);

            if (shipmentCompany == null) { return ServiceResult<bool>.NotFound("Shipment Company not found."); }

            mapper.Map(shipmentCompanyDTO, shipmentCompany);
            _repository.Update(shipmentCompany);
            return ServiceResult<bool>.Ok(true);
        }
    }
}
