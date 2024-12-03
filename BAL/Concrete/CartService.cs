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
            foreach (var item in items)
            {
                itemsDTO.Cart.Add(mapper.Map<CartItemDTO>(item));
            }
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
                _repository.Insert(mapper.Map<CartItem>(cartItemDTO));
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
