using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(EcommerceContext _db) : base(_db)
        {

        }

        public IEnumerable<Order> GetOrdersByUserIdAsync(string userId)
        {
            return db.Orders
           .Include(o => o.OrderItems)
           .Where(o => o.UserId == userId)
           .ToList();
        }
    }
}
