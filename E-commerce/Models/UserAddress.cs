using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        public string? City { get; set;}

        public string? Country { get; set; }

        public int? Mobile { get; set; }

        public int? Postalcode { get; set; }

        public string? Address { get; set;}

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public ApplicationUser? User { get; set; }




    }
}
