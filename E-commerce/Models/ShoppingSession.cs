using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class ShoppingSession
    {
        public int Id { get; set; }

        public int? Total { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public ApplicationUser? User { get; set;}

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();


    }
}
