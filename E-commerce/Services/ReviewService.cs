
using AutoMapper;
using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Services
{
    public class ReviewService
    {
        private UnitOfWork unit;
        private readonly IMapper mapper;

        public ReviewService(UnitOfWork _unit,IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }


        public List<ReviewDto> GetAll()
        {
            var reviews = unit.ReviewRepository.GetAll();

            return mapper.Map<List<ReviewDto>>(reviews);
        }


        //public ReviewDto GetById(int id)
        //{
        //    var review = unit.ReviewRepository.GetById(id);

        //    return mapper.Map<ReviewDto>(review);
        //}


        public List<ReviewDto> GetProductReviews(int productId)
        {

            return unit.ReviewRepository.GetProductReviews(productId);
        }

        public void PostReview(ReviewDto reviewDto)
        {
            var review = mapper.Map<Review>(reviewDto);

            unit.ReviewRepository.Add(review);
            unit.ReviewRepository.Save();
        }

        public void Delete(int id)
        {
            unit.ReviewRepository.Delete(id);
        }


    }
}
