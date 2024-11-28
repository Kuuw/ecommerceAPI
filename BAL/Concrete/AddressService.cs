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
        public List<Address> GetByUserId(int UserId)
        {
            var adresses = addressRepository.Where(x => x.UserId == UserId).ToList();

            return adresses;
        }

        public Address? GetByAddressId(int AddressId)
        {
            var address = addressRepository.GetById(AddressId);
            return address;
        }

        public Address Add(Address address)
        {
            address.CreatedAt = DateTime.Now;
            address.UpdatedAt = DateTime.Now;

            addressRepository.Insert(address);
            return address;
        }

        public void Delete(Address address)
        {
            addressRepository.Delete(address);
        }

        public Address Update(Address address)
        {
            address.UpdatedAt = DateTime.Now;
            addressRepository.Update(address);
            return address;
        }
    }
}
