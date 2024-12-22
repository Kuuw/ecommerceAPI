using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public AddressService(IAddressRepository repository, IUserContext userContext)
        {
            _addressRepository = repository;
            _userContext = userContext;
        }

        public List<AddressDTO> GetByUserId()
        {
            var adresses = _addressRepository.Where(x => x.UserId == _userContext.UserId).ToList();
            var addressDTOs = new List<AddressDTO>();

            foreach (var address in adresses)
            {
                addressDTOs.Add(mapper.Map<AddressDTO>(address));
            }

            return addressDTOs;
        }

        public AddressDTO? GetByAddressId(int AddressId)
        {
            var address = _addressRepository.GetById(AddressId);
            if (address?.UserId == _userContext.UserId)
            {
                return mapper.Map<AddressDTO>(address);
            }
            return null;
        }

        public Address Add(AddressDTO addressDTO)
        {
            addressDTO.UserId = _userContext.UserId;
            var address = mapper.Map<Address>(addressDTO);

            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Insert(address);
            return address;
        }

        public bool Delete(int addressID)
        {
            var address = _addressRepository.GetById(addressID);
            if (address != null && address.UserId == _userContext.UserId)
            {
                _addressRepository.Delete(address);
                return true;
            }
            return false;
        }

        public bool Update(AddressDTO addressDTO)
        {
            var address = _addressRepository.GetById((int)addressDTO.AddressId!);
            if (address == null) { return false; }
            if (address.UserId != _userContext.UserId) { return false; }

            mapper.Map(addressDTO, address);
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Update(address);
            return true;
        }
    }
}
