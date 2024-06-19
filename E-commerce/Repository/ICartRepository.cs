using E_commerce.Models;

namespace E_commerce.Repository
{
    public interface ICartRepository : IRepository<Cart>
    {

         Cart GetCartByUserId(string userId);

        //Task<Cart> AddCart(Cart Cart);

        //Task<Cart> UpdateCart(Cart Cart);

        //Task<Cart> RemoveCart(int CartId);

    }
}
