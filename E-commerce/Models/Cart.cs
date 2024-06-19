using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<CartItem>? CartItems { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
