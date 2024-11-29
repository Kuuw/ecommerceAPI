using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public DateTime CreatedAt { get; set; }

    public DateTime ShippedAt { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ShipmentCompany? ShipmentCompany { get; set; }

    public virtual User User { get; set; }
}
