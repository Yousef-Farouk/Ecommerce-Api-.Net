using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class ProductSize
    {

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string? Size {  get; set; }

        public virtual Product? Product { get; set; }



    }
}
