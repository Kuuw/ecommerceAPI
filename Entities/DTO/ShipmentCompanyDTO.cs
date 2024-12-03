using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ShipmentCompanyDTO
    {
        public int? ShipmentCompanyId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? CompanySite { get; set; }

        public string? CompanyLogoUrl { get; set; }
    }
}
