using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int? UserId { get; set; }

    public int? CountryId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? PostalCode { get; set; }

    public string? Telephone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
