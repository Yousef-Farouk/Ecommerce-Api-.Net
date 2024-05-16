using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTOS
{
    public class ProductDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public int? Price { get; set; }

        public IFormFile Image { get; set; }

        public string? ImageUrl {  get; set; }

        public int? CategoryId { get; set; }

        public int? CouponId { get; set; }


    }
}
