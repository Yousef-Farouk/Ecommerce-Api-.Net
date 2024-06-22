using E_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.DTOS
{
    public class OrderItemDto
    {

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public int? ProductId { get; set; }

    }
}
