using Entities.Models;

namespace BAL.Abstract
{
    public interface IAddressService
    {
        public List<Address> GetByUserId(int UserId);
        public Address? GetByAddressId(int addressId);
        public Address Add(Address address);
    }
}
