namespace Entities.DTO
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        
        public int AddressId { get; set; }

        public int? ShipmentCompanyId { get; set; }

        public string? ShipmentTrack { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
