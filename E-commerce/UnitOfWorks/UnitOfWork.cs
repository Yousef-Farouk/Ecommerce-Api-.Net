using CloudinaryDotNet;
using E_commerce.Models;
using E_commerce.Repository;
using Task.Repository;

namespace E_commerce.UnitOfWorks
{
    public class UnitOfWork
    {
        EcommerceContext db;
        IProductRepository productRepository;
        ICategoryRepository categoryRepository;
        private readonly Cloudinary cloudinary;
        

        public UnitOfWork(EcommerceContext _db,Cloudinary _cloudinary)
        {
            db = _db;
            cloudinary = _cloudinary;
        }


        public IProductRepository ProductRepository 
        { 
            get { 
                if (productRepository == null)
                {
                     productRepository = new ProductRepository(db,cloudinary); 

                }
                return productRepository;
            } 
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if(categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }

                return categoryRepository;
            }

        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
