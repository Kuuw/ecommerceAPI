using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public int? CountryPhoneCode { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
