using E_commerce.DTOS;
using E_commerce.Models;

namespace E_commerce.Repository
{
    public interface IReviewRepository : IRepository<Review>
    {

        public List<ReviewDto> GetProductReviews(int productId);


    }
}
