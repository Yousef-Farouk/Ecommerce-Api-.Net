using E_commerce.Models;
using E_commerce.Repository;

namespace Task.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<bool> DeleteImage(string imageUrl);

        string GetPublicIdFromImageUrl(string imageUrl);
    }
}
