using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class ShipmentCompanyRepository:GenericRepository<ShipmentCompany>, IShipmentCompanyRepository
    {
    }
}
