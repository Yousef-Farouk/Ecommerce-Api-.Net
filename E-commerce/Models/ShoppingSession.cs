using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class ShoppingSession
    {
        public int Id { get; set; }

        public int? Total { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set;}

        public virtual List<CartItem>? CartItems { get; set; } = new List<CartItem>();


    }
}
