using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class UserPayment
    {
        public int Id { get; set; }

        public string? PaymentMethod { get; set; }

        [Column(TypeName ="Date")]
        public DateTime? PaymentDate { get; set; }

        public int? TotalPrice {  get; set; }


        [ForeignKey("User")]
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }


        [ForeignKey("Order")]
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }

    }
}
