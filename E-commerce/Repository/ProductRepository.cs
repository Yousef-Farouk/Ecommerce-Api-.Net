using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_commerce.Models;
using E_commerce.UnitOfWorks;
using System.Net;
using Task.Repository;

namespace E_commerce.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly Cloudinary cloudinary;

        public ProductRepository(EcommerceContext db,Cloudinary _cloudinary) : base(db) 
        {
            cloudinary = _cloudinary;
        }


        public async Task<bool> DeleteImage(string imageUrl)
        {

            // Parse the public ID of the image from its URL
            var publicId = GetPublicIdFromImageUrl(imageUrl);

            var folder = "cozastore_Photos";

            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> {  $"{folder}/{publicId}" },
                Type = "upload",
                ResourceType = ResourceType.Image
            };

            //var deletionResult = await cloudinary.DestroyAsync(deletionParams);
            var deletionResult = cloudinary.DeleteResources(deleteParams);
            // Check if deletion was successful
            return deletionResult.StatusCode == HttpStatusCode.OK;

        }

        public string GetPublicIdFromImageUrl(string imageUrl)
        {

            var publicId = imageUrl.Split("/").Last().Split(".").First();
            return publicId;
        }

        public List<Product> GetProductsByCategoy(string categroy) 
        { 
            return  db.Products.Where(p=>p.Category.Name == categroy).ToList();
        
        }



    }
}
//var deleteParams = new DelResParams()
//{
//    PublicIds = new List<string> { "cozastore_Photos/letdldcrjjj5plzmezzx" },
//    Type = "upload",
//    ResourceType = ResourceType.Image
//};
//var result = cloudinary.DeleteResources(deleteParams);
//Console.WriteLine(result.JsonObj);