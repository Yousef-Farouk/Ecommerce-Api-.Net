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
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual UserPayment? Payment { get; set; }

        public virtual List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();




    }
}
