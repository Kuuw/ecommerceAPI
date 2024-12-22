using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IAddressService
    {
        public ServiceResult<List<AddressDTO>> GetByUserId();
        public ServiceResult<AddressDTO?> GetByAddressId(int AddressId);
        public ServiceResult<bool> Add(AddressDTO addressDTO);
        public ServiceResult<bool> Delete(int addressID);
        public ServiceResult<bool> Update(AddressDTO addressDTO);
    }
}
