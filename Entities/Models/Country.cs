using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public partial class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public int? CountryPhoneCode { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
