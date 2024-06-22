using E_commerce.Models;

namespace E_commerce.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
       IEnumerable<Order> GetOrdersByUserIdAsync(string userId);

    }
}
