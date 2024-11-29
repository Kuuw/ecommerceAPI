using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public partial class ShipmentCompany
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShipmentCompanyId { get; set; }

    public string CompanyName { get; set; }

    public string? CompanySite { get; set; }

    public string? CompanyLogoUrl { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
