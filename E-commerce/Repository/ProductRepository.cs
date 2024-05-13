using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_commerce.Models;
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

            // Delete the image from Cloudinary
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await cloudinary.DestroyAsync(deletionParams);

            // Check if deletion was successful
            return deletionResult.Result == "ok";
           
        }

        public string GetPublicIdFromImageUrl(string imageUrl)
        {
           
            var publicId = imageUrl.Split("/").Last().Split(".").First();
            return publicId;
        }



    }
}
