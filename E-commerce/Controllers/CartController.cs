﻿using E_commerce.DTOS;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly CartService cartService;

        public CartController(CartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            var cartDto = await cartService.GetCartByUserId(userId);
            if (cartDto == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cartDto);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddProductToCart(string userId,  AddProductToCartDto dto)
        {
            cartService.AddProductToCart(userId, dto.ProductId, dto.Quantity);

            return Ok("Product added successfully");
        }

    }
}
