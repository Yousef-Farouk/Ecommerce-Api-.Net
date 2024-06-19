using E_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.DTOS
{
    public class CartDto
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public List<CartItemDto>? CartItems { get; set; }
    }
}
