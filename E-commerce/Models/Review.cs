using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string? Feedback { get; set; }

        public int? Rating { get; set; }


        [ForeignKey("User")]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }


        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }

    }
}
