using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Boolean? Active { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? ExpirationDate { get; set; }

        public int? DiscountAmount { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();



    }
}
