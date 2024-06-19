using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Models
{
    public class EcommerceContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<CartItem>? CartItems { get; set; }

        public DbSet<Cart>? Carts { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Coupon>? Coupons { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<OrderItem>? OrderItems { get; set;}

        public DbSet<Product>? Products { get; set;}

        public DbSet<Review>? Reviews { get; set; }

        public DbSet<ShoppingSession>? ShoppingSessions { get; set; }

        public DbSet<UserAddress>? UserAddresses { get; set; }

        public DbSet<UserPayment>? UserPayments { get; set; }

        public DbSet<ProductSize>? ProductSizes { get; set; }

        public EcommerceContext() :base() {}

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductSize>().HasKey("ProductId", "Size");
            base.OnModelCreating(modelBuilder);
        }

    }
}
