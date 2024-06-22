using AutoMapper;
using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.Repository;
using E_commerce.UnitOfWorks;

namespace E_commerce.Services
{
    public class OrderService
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public OrderService(IMapper _mapper, UnitOfWork _unit)
        {
            unit= _unit;
            mapper = _mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = unit.OrderRepository.GetById(id);

            return mapper.Map<OrderDto>(order);
        }

        public IEnumerable<OrderDto> GetOrdersByUserIdAsync(string userId)
        {
            var orders = unit.OrderRepository.GetOrdersByUserIdAsync(userId);
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto CreateOrderAsync(string userId, CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Price = (int?)createOrderDto.OrderItems.Sum(i => i.Quantity * i.Price),
                OrderItems = createOrderDto.OrderItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            unit.OrderRepository.Add(order);
            //unit.SaveChanges();

            var cartItems = unit.CartRepository.GetCartByUserId(order.UserId).CartItems;

            foreach (var orderItem in order.OrderItems)
            {
                var cartItem = cartItems.FirstOrDefault(ci => ci.ProductId == orderItem.ProductId);
                if (cartItem != null)
                {
                    unit.CartItemRepository.Delete(cartItem);

                }
            }

            unit.SaveChanges();

            return mapper.Map<OrderDto>(order);
        }

    }
}
