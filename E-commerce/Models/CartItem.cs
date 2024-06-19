using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }

        [ForeignKey("ShoppingSession")]
        public int? SessionId { get; set; }

        public virtual ShoppingSession? ShoppingSession { get; set; }


        [ForeignKey("Cart")]
        public int? CartId { get; set; }

        public virtual Cart? Cart { get; set; }


    }
}
