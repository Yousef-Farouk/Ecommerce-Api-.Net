using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string? Feedback { get; set; }

        public int? Rating { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }

        public ApplicationUser? User { get; set; }

    }
}
