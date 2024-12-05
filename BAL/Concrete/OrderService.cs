using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public OrderDTO Add(OrderDTO orderDTO, int userId)
        {
            orderDTO.OrderId = null;
            var order = mapper.Map<Order>(orderDTO);
            order.CreatedAt = DateTime.UtcNow;
            _repository.Insert(order);

            foreach (var item in orderDTO.Items) 
            {
                _repository.InsertItem(mapper.Map<OrderItem>(item));
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
            var orders = _repository.Where(x => x.UserId == userId);
            var ordersDTO = new List<OrderDTO>();
            foreach (var item in orders)
            {
                ordersDTO.Add(mapper.Map<OrderDTO>(item));
            }
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
                orderDTO.Items = orderItemsDTO;

                return orderDTO;
            }
            return null;
        }
    }
}
