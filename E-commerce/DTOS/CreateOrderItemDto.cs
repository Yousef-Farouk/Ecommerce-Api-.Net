﻿namespace E_commerce.DTOS
{
    public class CreateOrderItemDto
    {

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
