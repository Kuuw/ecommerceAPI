using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public OrderService(IOrderRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public ServiceResult<OrderDTO> Add(OrderDTO orderDTO)
        {
            orderDTO.OrderId = null;
            var order = mapper.Map<Order>(orderDTO);
            order.UserId = _userContext.UserId;
            _repository.Insert(order);

            return ServiceResult<OrderDTO>.Ok(orderDTO);
        }

        public ServiceResult<bool> Delete(int id)
        {
            var order = _repository.GetById(id);
            if (order != null)
            {
                _repository.Delete(order);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.NotFound("Order not found.");
        }

        public ServiceResult<List<OrderDTO>> Get()
        {
            var orders = _repository.Get(_userContext.UserId);
            List<OrderDTO> ordersDTO = mapper.Map<List<OrderDTO>>(orders);

            return ServiceResult<List<OrderDTO>>.Ok(ordersDTO);
        }

        public ServiceResult<OrderDTO?> GetById(int id)
        {
            var orderDTO = new OrderDTO();
            var order = _repository.GetById(id);
            if (order != null && order.UserId == _userContext.UserId)
            {
                mapper.Map(order, orderDTO);

                return ServiceResult<OrderDTO?>.Ok(orderDTO);
            }
            return ServiceResult<OrderDTO?>.NotFound("Order not found.");
        }
    }
}
