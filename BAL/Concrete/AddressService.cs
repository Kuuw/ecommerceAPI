using BAL.Abstract;
using DAL.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concrete
{
    public class AddressService: IAddressService
    {
        AddressRepository addressRepository = new AddressRepository();
        public List<Address> GetAddresses(int UserId)
        {
            var adresses = addressRepository.Where(x => x.UserId == UserId).ToList();

            return adresses;
        }

        public Address AddAddress(Address address)
        {
            address.CreatedAt = DateTime.Now;
            address.UpdatedAt = DateTime.Now;

            addressRepository.Insert(address);
            return address;
        }
    }
}
