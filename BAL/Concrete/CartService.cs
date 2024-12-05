using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public CartDTO Get(int userId)
        {
            var items = _repository.Where(x => x.UserId == userId);
            var itemsDTO = new CartDTO();
            List<CartItemDTO> CartDTO = new();
            mapper.Map(items, CartDTO);
            itemsDTO.Cart = CartDTO;
            return itemsDTO;
        }

        public void Update(int userId, CartItemDTO cartItemDTO)
        {
            var cartItem = _repository.Where(x => x.UserId == userId && x.ProductId == cartItemDTO.ProductId).FirstOrDefault();
            if (cartItem != null) 
            {
                mapper.Map(cartItemDTO, cartItem);
                _repository.Update(cartItem);
            }
            else
            {
                var newCartItem = mapper.Map<CartItem>(cartItemDTO);
                newCartItem.UserId = userId;
                _repository.Insert(newCartItem);
            }
        }

        public void Delete(int userId, int productId) 
        {
            var cartItem = _repository.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            if (cartItem != null) 
            {
                _repository.Delete(cartItem);
            }
        }
    }
}
