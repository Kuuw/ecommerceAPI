using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public CartService(ICartRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public CartDTO Get()
        {
            var items = _repository.ListWithProductData(_userContext.UserId);
            var itemsDTO = new CartDTO();
            List<CartItemDTO> cartItemDTO = new();
            mapper.Map(items, cartItemDTO);
            itemsDTO.Cart = cartItemDTO;
            return itemsDTO;
        }

        public void Update(CartItemDTO cartItemDTO)
        {
            var cartItem = _repository.Where(x => x.UserId == _userContext.UserId && x.ProductId == cartItemDTO.ProductId).FirstOrDefault();
            if (cartItem != null) 
            {
                mapper.Map(cartItemDTO, cartItem);
                _repository.Update(cartItem);
            }
            else
            {
                var newCartItem = mapper.Map<CartItem>(cartItemDTO);
                newCartItem.UserId = _userContext.UserId;
                _repository.Insert(newCartItem);
            }
        }

        public void Delete(int productId) 
        {
            var cartItem = _repository.Where(x => x.UserId == _userContext.UserId && x.ProductId == productId).FirstOrDefault();
            if (cartItem != null) 
            {
                _repository.Delete(cartItem);
            }
        }
    }
}
