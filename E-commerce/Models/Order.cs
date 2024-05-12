using Azure.Core.Pipeline;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? OrderDate { get; set; }

        public int? Price {get; set;}


        [ForeignKey("User")]
        public int? UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public UserPayment? Payment { get; set; }

        public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();




    }
}
