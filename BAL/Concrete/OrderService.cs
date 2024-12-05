using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public OrderService(IOrderRepository repository, IProductRepository productService)
        {
            _productRepository = productService;
            _repository = repository;
        }

        public OrderDTO Add(OrderDTO orderDTO, int userId)
        {
            orderDTO.OrderId = null;
            var order = mapper.Map<Order>(orderDTO);
            order.UserId = userId;
            order.CreatedAt = DateTime.Now;
            _repository.Insert(order);

            foreach (var item in orderDTO.OrderItems) 
            {
                var productStock = _productRepository.GetStock(item.ProductId);
                if (productStock == null) { throw new Exception("Product not found"); }
                if (productStock.Stock < item.Quantity) { throw new Exception("Not enough stock"); }
                if (productStock.Stock == 0) { throw new Exception("Out of stock"); }

                var productPrice = _productRepository.GetById(item.ProductId)!.UnitPrice;
                item.UnitPrice = productPrice; // Set the unit price of the product so that the price cannot be changed by the user.

                var orderItem = mapper.Map<OrderItem>(item);
                orderItem.OrderId = order.OrderId;

                _productRepository.UpdateStock(item.ProductId, productStock.Stock-item.Quantity);
                _repository.InsertItem(orderItem);
            }
            return orderDTO;
        }

        public void Delete(int id)
        {
            var order = _repository.GetById(id);
            if (order != null)
            {
                _repository.Delete(order);
            }
        }

        public List<OrderDTO> Get(int userId)
        {
            var orders = _repository.Get(userId);
            List<OrderDTO> ordersDTO = mapper.Map<List<OrderDTO>>(orders);

            return ordersDTO;
        }

        public OrderDTO? GetById(int id, int userId)
        {
            var orderDTO = new OrderDTO();
            var order = _repository.GetById(id);
            if (order != null && order.UserId == userId)
            {
                mapper.Map(order, orderDTO);
                var orderItems = _repository.GetItems(order.OrderId);
                var orderItemsDTO = new List<OrderItemDTO>();
                foreach(var item in orderItems)
                {
                    orderItemsDTO.Add(mapper.Map<OrderItemDTO>(item));
                }
                orderDTO.OrderItems = orderItemsDTO;

                return orderDTO;
            }
            return null;
        }
    }
}
