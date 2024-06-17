using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_commerce.Repository
{
    public class ReviewRepository : Repository<Review> , IReviewRepository
    {
        private readonly IMapper mapper ;

        public ReviewRepository(EcommerceContext db ,IMapper _mapper) : base(db)
        {
           mapper = _mapper;
        }

        public List<ReviewDto> GetProductReviews(int productId)
        {
            return db.Reviews
              .Where(r => r.ProductId == productId)
              .ProjectTo<ReviewDto>(mapper.ConfigurationProvider)
              .ToList();


        }
    }
}
