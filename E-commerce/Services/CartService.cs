using AutoMapper;
using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.UnitOfWorks;

namespace E_commerce.Services
{
    public class CartService
    {

        private UnitOfWork unit;
        private readonly IMapper mapper;

        public CartService(UnitOfWork _unit, IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            var cart = unit.CartRepository.GetCartByUserId(userId);

            if (cart == null) return null;

            var cartDto = mapper.Map<CartDto>(cart);

            return cartDto;
        }

        public  void AddProductToCart(string userId , int productId , int quantity)
        {
            var cart =  unit.CartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                unit.CartRepository.Add(cart);
            }


            var cartItem = cart.CartItems.FirstOrDefault(ct=>ct.ProductId == productId);

            if (cartItem == null) 
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                
                cart.CartItems.Add(cartItem);
                unit.SaveChanges();

            }

            else
            {
                cartItem.Quantity += quantity;
            }

            unit.CartRepository.Update(cart);
            unit.SaveChanges();
        }

        public void DeleteProductFromCart(string userId ,int ProductId)
        {
            var cart = unit.CartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                throw new Exception("Cart not found");

            }

            var CartItem = cart.CartItems.FirstOrDefault(ct=>ct.ProductId == ProductId);
            if (CartItem == null)
            {
                throw new Exception("Product not found in cart");
            }

            //cart.CartItems.Remove(CartItem);
            unit.CartItemRepository.Delete(CartItem);
            unit.CartRepository.Update(cart);
            unit.SaveChanges();

        }



    }
}
