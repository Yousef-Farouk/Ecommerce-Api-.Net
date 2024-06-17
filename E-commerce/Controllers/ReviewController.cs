using E_commerce.DTOS;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService reviewService;

        public ReviewController(ReviewService _reviewService)
        {
            this.reviewService = _reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = reviewService.GetAll();
            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductReviews(int id)
        {
            var reviews = reviewService.GetProductReviews(id);
            if (reviews == null)
            {
                return NotFound("No Reviews Related to this product");
            }

            return Ok(reviews);
        }


        [HttpPost]
        public async Task<IActionResult> PostReview(ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            reviewService.PostReview(reviewDto);

            return Ok(new { message="Review Posted Successfully" });
        }






    }
}
