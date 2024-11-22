using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class AddressRepository:GenericRepository<Address>, IAddressRepository
    {
    }
}
