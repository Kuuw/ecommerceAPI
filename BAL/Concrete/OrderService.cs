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
            _repository.Insert(order);

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
