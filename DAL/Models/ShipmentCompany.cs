using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ShipmentCompany
{
    public int ShipmentCompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanySite { get; set; }

    public string? CompanyLogoUrl { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
