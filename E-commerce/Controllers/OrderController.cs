using E_commerce.DTOS;
using E_commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderService orderService;

        public OrderController(OrderService _orderService)
        {
            orderService = _orderService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrder(string userId, [FromBody] CreateOrderDto createOrderDto)
        {
            var orderDto = orderService.CreateOrderAsync(userId, createOrderDto);
            return Ok(orderDto);
        }

        [HttpGet("{userId:alpha}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var orderDtos = orderService.GetOrdersByUserIdAsync(userId);

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orderDto = orderService.GetOrderByIdAsync(id);
            if (orderDto == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            return Ok(orderDto);
        }
    }
}
