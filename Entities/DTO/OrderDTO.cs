namespace Entities.DTO
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        
        public int AddressId { get; set; }

        public int? ShipmentCompanyId { get; set; }

        public string? ShipmentTrack { get; set; }

        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

        public AddressDTO? Address { get; set; }
    }
}
