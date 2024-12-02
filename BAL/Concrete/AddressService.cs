using AutoMapper;
using BAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class AddressService : IAddressService
    {
        AddressRepository addressRepository = new AddressRepository();
        Mapper mapper = MapperConfig.InitializeAutomapper();

        public List<AddressDTO> GetByUserId(int UserId)
        {
            var adresses = addressRepository.Where(x => x.UserId == UserId).ToList();
            var addressDTOs = new List<AddressDTO>();

            foreach (var address in adresses)
            {
                addressDTOs.Add(mapper.Map<AddressDTO>(address));
            }

            return addressDTOs;
        }

        public AddressDTO? GetByAddressId(int AddressId)
        {
            var address = addressRepository.GetById(AddressId);
            return mapper.Map<AddressDTO>(address);
        }

        public Address Add(AddressDTO addressDTO)
        {
            var address = mapper.Map<Address>(addressDTO);

            address.CreatedAt = DateTime.Now;
            address.UpdatedAt = DateTime.Now;

            addressRepository.Insert(address);
            return address;
        }

        public bool Delete(int addressID, int userId)
        {
            var address = addressRepository.GetById(addressID);
            if (address != null && address.UserId == userId)
            {
                addressRepository.Delete(address);
                return true;
            }
            return false;
        }

        public bool Update(AddressDTO addressDTO, int addressId, int userId)
        {
            var address = addressRepository.GetById(addressId);
            if (address == null) { return false; }
            if (address.UserId != userId) { return false; }

            mapper.Map(addressDTO, address);
            address.UpdatedAt = DateTime.Now;

            addressRepository.Update(address);
            return true;
        }
    }
}
