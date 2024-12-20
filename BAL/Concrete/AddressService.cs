using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public AddressService(IAddressRepository repository)
        {
            _addressRepository = repository;
        }

        public List<AddressDTO> GetByUserId(int UserId)
        {
            var adresses = _addressRepository.Where(x => x.UserId == UserId).ToList();
            var addressDTOs = new List<AddressDTO>();

            foreach (var address in adresses)
            {
                addressDTOs.Add(mapper.Map<AddressDTO>(address));
            }

            return addressDTOs;
        }

        public AddressDTO? GetByAddressId(int AddressId, int userId)
        {
            var address = _addressRepository.GetById(AddressId);
            if (address?.UserId == userId)
            {
                return mapper.Map<AddressDTO>(address);
            }
            return null;
        }

        public Address Add(AddressDTO addressDTO)
        {
            var address = mapper.Map<Address>(addressDTO);

            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Insert(address);
            return address;
        }

        public bool Delete(int addressID, int userId)
        {
            var address = _addressRepository.GetById(addressID);
            if (address != null && address.UserId == userId)
            {
                _addressRepository.Delete(address);
                return true;
            }
            return false;
        }

        public bool Update(AddressDTO addressDTO, int userId)
        {
            var address = _addressRepository.GetById((int)addressDTO.AddressId!);
            if (address == null) { return false; }
            if (address.UserId != userId) { return false; }

            mapper.Map(addressDTO, address);
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Update(address);
            return true;
        }
    }
}
