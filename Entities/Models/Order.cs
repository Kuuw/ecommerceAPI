using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public partial class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public int? ShipmentCompanyId { get; set; }

    public string? ShipmentTrack { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime ShippedAt { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ShipmentCompany? ShipmentCompany { get; set; }

    public virtual User User { get; set; } = null!;
}
