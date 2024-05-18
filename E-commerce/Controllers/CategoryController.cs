using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly UnitOfWork unit;

        public CategoryController(UnitOfWork _unit)
        {
            unit = _unit;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = unit.CategoryRepository.GetAll();
            var categoryDto = categories.Select(c => new CategoryDto
            { 
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();


            return Ok(categoryDto);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = unit.CategoryRepository.GetById(id);
            if(category == null) 
            {
                return NotFound();
            }
            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,

            };

            return Ok(categoryDto);
        }

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetById(string name )
        //{
        //    var category = unit.CategoryRepository.GetById(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categoryDto = new CategoryDto()
        //    {
        //        Name = category.Name,
        //        Description = category.Description,

        //    };

        //    return Ok(categoryDto);
        //}

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                };

                unit.CategoryRepository.Add(category);
                unit.SaveChanges();
                return Ok("Category added successfully");
            }

            return BadRequest(ModelState);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,CategoryDto categoryDto)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = unit.CategoryRepository.GetById(id);

            if (unit.CategoryRepository.GetById(id) == null)
                return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
           

            unit.CategoryRepository.Update(category);
                unit.SaveChanges();

            return Ok(category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = unit.CategoryRepository.GetById(id);

            if (unit.CategoryRepository.GetById(id) == null)
                return NotFound();

            unit.CategoryRepository.Delete(id);
            unit.SaveChanges();

            return Ok(category);
        }
    }
}
