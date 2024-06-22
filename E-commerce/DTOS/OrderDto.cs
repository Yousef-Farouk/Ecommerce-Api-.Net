using E_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.DTOS
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Price { get; set; }

        public string? UserId { get; set; }

        public virtual List<OrderItemDto>? OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
