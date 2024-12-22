using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IShipmentCompanyService
    {
        public ServiceResult<List<ShipmentCompanyDTO>> Get();
        public ServiceResult<ShipmentCompanyDTO?> GetById(int id);
        public ServiceResult<bool> Update(ShipmentCompanyDTO shipmentCompanyDTO);
        public ServiceResult<bool> Delete(int id);
        public ServiceResult<ShipmentCompanyDTO> Add(ShipmentCompanyDTO shipmentCompanyDTO);
    }
}
