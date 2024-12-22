using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IAddressService
    {
        public List<AddressDTO> GetByUserId();
        public AddressDTO? GetByAddressId(int AddressId);
        public Address Add(AddressDTO addressDTO);
        public bool Delete(int addressID);
        public bool Update(AddressDTO addressDTO);
    }
}
