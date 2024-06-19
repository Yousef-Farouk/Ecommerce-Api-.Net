using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        public CartRepository(EcommerceContext _db) : base(_db)
        {


        }

       
        public Cart GetCartByUserId(string userId)
        {
            return  db.Carts.Include(c => c.CartItems).ThenInclude(ci=> ci.Product).FirstOrDefault(c => userId == c.UserId);
        }

        
    }
}
