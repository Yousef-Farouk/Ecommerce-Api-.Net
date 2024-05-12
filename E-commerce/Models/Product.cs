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

        public Order? OrderItem { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set;}


        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }

        public Coupon? Coupon { get; set; }


        public CartItem? CartItem { get; set; }

    }
}
