using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public int? Price { get; set; }

        public string? Image {  get; set; }

        public virtual List<Order>? OrderItems { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set;}


        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }

        public virtual Coupon? Coupon { get; set; }


        public virtual List<CartItem>? CartItems { get; set; }

        public virtual List<Review>? Reviews { get; set; } 

    }
}
