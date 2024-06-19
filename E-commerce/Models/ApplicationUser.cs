using Microsoft.AspNetCore.Identity;

namespace E_commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string? UserName { get; set; }

        public string? Gender { get; set; }

        public string? Image { get; set; }

        public virtual  UserAddress? Address { get; set;}
          
        public virtual ShoppingSession? ShoppingSession { get; set;}
               
        public virtual List<UserPayment>? UserPayement { get; set; } = new List<UserPayment>();
               
        public virtual List<Review>? Reviews { get; set; } = new List<Review>();
                
        public virtual List<Order>? Orders { get; set; } = new List<Order>();

        public virtual Cart Cart { get; set; }

    }
}
