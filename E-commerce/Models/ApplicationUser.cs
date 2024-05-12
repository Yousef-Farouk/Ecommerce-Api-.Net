using Microsoft.AspNetCore.Identity;

namespace E_commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? UserName { get; set; }

        public string? Gender { get; set; }

        public string? Image { get; set; }

        public UserAddress? Address { get; set;}

        public List<UserPayment>? UserPayement { get; set; } = new List<UserPayment>();

        public List<Review>? Review { get; set; } = new List<Review>();

        public List<Order>? Order { get; set; } = new List<Order>();

        public ShoppingSession? ShoppingSession { get; set;}



    }
}
