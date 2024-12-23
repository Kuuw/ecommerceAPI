using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
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

        public ServiceResult<List<AddressDTO>> GetByUserId()
        {
            var adresses = _addressRepository.Where(x => x.UserId == _userContext.UserId).ToList();
            var addressDTOs = new List<AddressDTO>();

            foreach (var address in adresses)
            {
                addressDTOs.Add(mapper.Map<AddressDTO>(address));
            }

            var result = ServiceResult<List<AddressDTO>>.Ok(addressDTOs);

            return result;
        }

        public ServiceResult<AddressDTO?> GetByAddressId(int AddressId)
        {
            var address = _addressRepository.GetById(AddressId);
            if (address?.UserId == _userContext.UserId)
            {
                var result = ServiceResult<AddressDTO?>.Ok(mapper.Map<AddressDTO>(address));

                return result;
            }
            return ServiceResult<AddressDTO?>.NotFound("Not found or belongs to another user.");
        }

        public ServiceResult<bool> Add(AddressDTO addressDTO)
        {
            addressDTO.UserId = _userContext.UserId;
            var address = mapper.Map<Address>(addressDTO);

            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Insert(address);
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> Delete(int addressID)
        {
            var address = _addressRepository.GetById(addressID);
            if (address != null && address.UserId == _userContext.UserId)
            {
                _addressRepository.Delete(address);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.BadRequest("Not found or belongs to another user.");
        }

        public ServiceResult<bool> Update(AddressDTO addressDTO)
        {
            var address = _addressRepository.GetById((int)addressDTO.AddressId!);
            if (address == null || address.UserId != _userContext.UserId) { return ServiceResult<bool>.NotFound("Address not found or belongs to another user."); }

            mapper.Map(addressDTO, address);
            address.UpdatedAt = DateTime.UtcNow;

            _addressRepository.Update(address);
            return ServiceResult<bool>.Ok(true);
        }
    }
}
