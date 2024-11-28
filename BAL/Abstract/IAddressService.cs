using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IAddressService
    {
        public List<Address> GetByUserId(int UserId);
        public Address? GetByAddressId(int addressId);
        public Address Add(Address address);
    }
}
