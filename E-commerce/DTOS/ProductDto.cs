using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTOS
{
    public class ProductDto
    {
        public int? id { get; set; }

        public string? name { get; set; }

        public string? description { get; set; }

        public int? quantity { get; set; }

        public int? price { get; set; }

        public IFormFile? image { get; set; }

        public string? imageUrl {  get; set; }

        public int? categoryId { get; set; }

        public int? couponId { get; set; }


    }
}
