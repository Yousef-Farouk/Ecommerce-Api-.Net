using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set;}


        [ForeignKey("Order")]
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }


        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }


    }
}
