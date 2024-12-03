using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IShipmentCompanyService
    {
        public List<ShipmentCompanyDTO> Get();
        public ShipmentCompanyDTO? GetById(int id);
        public void Update(ShipmentCompanyDTO shipmentCompanyDTO);
        public void Delete(int id);
        public ShipmentCompanyDTO Add(ShipmentCompanyDTO shipmentCompanyDTO);
    }
}
