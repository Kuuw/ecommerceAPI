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

        public ServiceResult<CartDTO> Get()
        {
            var items = _repository.ListWithProductData(_userContext.UserId);
            var itemsDTO = new CartDTO();
            List<CartItemDTO> cartItemDTO = new();
            mapper.Map(items, cartItemDTO);
            itemsDTO.Cart = cartItemDTO;
            return ServiceResult<CartDTO>.Ok(itemsDTO);
        }

        public ServiceResult<bool> Update(CartItemDTO cartItemDTO)
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
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> Delete(int productId) 
        {
            var cartItem = _repository.Where(x => x.UserId == _userContext.UserId && x.ProductId == productId).FirstOrDefault();
            if (cartItem != null) 
            {
                _repository.Delete(cartItem);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.NotFound("Item not found.");
        }
    }
}
