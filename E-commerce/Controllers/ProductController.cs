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
                id = p.Id,
                name = p.Name,
                price = p.Price,
                description = p.Description,
                quantity = p.Quantity,
                imageUrl = p.Image,
                categoryId = p.CategoryId,
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
                id = product.Id,
                name = product.Name,
                price = product.Price,
                description = product.Description,
                quantity = product.Quantity,
                imageUrl = product.Image,
                categoryId = product.CategoryId
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
                File = new FileDescription(productDto.image.FileName, productDto.image.OpenReadStream()),
                Folder = "cozastore_Photos"
            };

            var uploadResult = await cloudinary.UploadAsync(uploadparams);

            if (uploadResult.Error != null)
            {
                return BadRequest("Failed to upload image to Cloudinary");
            }

            var imageUrl = uploadResult.Uri.ToString();

            var product = new Product
            {
                Name = productDto.name,
                Description = productDto.description,
                Price = productDto.price,
                Quantity = productDto.quantity,
                Image = imageUrl
                //CategoryId = productDto.CategoryId,
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

           ImageUploadResult uploadResult = null ;

            if (unit.ProductRepository.GetById(id) == null)
                return NotFound();


            if (productDto.image != null)
            {

                if(!string.IsNullOrEmpty(product.Image))
                {
                    await unit.ProductRepository.DeleteImage(product.Image);
                }

                var uploadparam = new ImageUploadParams
                {
                    File = new FileDescription(productDto.image.FileName, productDto.image.OpenReadStream()),
                    Folder = "cozastore_Photos"
                };

                uploadResult = await cloudinary.UploadAsync(uploadparam);


                if (uploadResult.Error != null)
                {
                    return BadRequest("Failed to upload image to Cloudinary");
                }

            }

            product.Name = productDto.name ?? product.Name;
            product.Description = productDto.description ?? product.Description;
            product.Quantity = productDto.quantity ?? product.Quantity;
            product.Price = productDto.price ?? product.Price;
            product.Image = uploadResult == null ? product.Image : uploadResult.Uri.ToString();
            product.CategoryId = productDto.categoryId ?? product.CategoryId;
            unit.ProductRepository.Update(product);
            unit.SaveChanges();
            return Ok(productDto);

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
