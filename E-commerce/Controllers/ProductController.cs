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
    public class ProductController : ControllerBase
    {
        private readonly Cloudinary cloudinary;
        private readonly UnitOfWork unit;

        public ProductController(Cloudinary _cloudinary , UnitOfWork _unit)
        {
            cloudinary = _cloudinary;
            unit = _unit;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products  = unit.ProductRepository.GetAll();

            if (products == null)
            {
                return NotFound();
            }

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = p.Quantity,
                ImageUrl = p.Image
            }).ToList();


            return Ok(productsDto);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var product = unit.ProductRepository.GetById(id);

            if (product == null)

                return NotFound();

            var productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                ImageUrl = product.Image
            };

            return Ok(productDto);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            
            }

            var uploadparams = new ImageUploadParams
            {
                File = new FileDescription(productDto.Image.FileName, productDto.Image.OpenReadStream()),
                Folder = "cozastore_Photos"
            };

            var uploadResult = await cloudinary.UploadAsync(uploadparams);

            if (uploadResult.Error != null)
            {
                return BadRequest("Failed to upload image to Cloudinary");
            }

            var imageurl = uploadResult.Uri.ToString();

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
                Image = imageurl
            };

            unit.ProductRepository.Add(product);
            unit.ProductRepository.Save();
            return Ok("Product created successfully");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,ProductDto productDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = unit.ProductRepository.GetById(id);

            if (unit.ProductRepository.GetById(id) == null)
                return NotFound();


            if (productDto.Image != null)
            {

                if(!string.IsNullOrEmpty(product.Image))
                {
                    await unit.ProductRepository.DeleteImage(product.Image);
                }

                var uploadparam = new ImageUploadParams
                {
                    File = new FileDescription(productDto.Image.FileName, productDto.Image.OpenReadStream()),
                    Folder = "cozastore_Photos"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadparam);


                if (uploadResult.Error != null)
                {
                    return BadRequest("Failed to upload image to Cloudinary");
                }



                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Quantity = productDto.Quantity;
                product.Price = productDto.Price;
                product.Image = uploadResult.Uri.ToString();
               
                unit.ProductRepository.Update(product);
                unit.SaveChanges();
            }

            return Ok(product);

        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = unit.ProductRepository.GetById(id);

            if (unit.ProductRepository.GetById(id) == null)
                return NotFound();


           
            if (!string.IsNullOrEmpty(product.Image))
            {
                    await unit.ProductRepository.DeleteImage(product.Image);
            }
            unit.ProductRepository.Delete(id);
            unit.SaveChanges();

            return Ok(product);
        }

           

    }

}
