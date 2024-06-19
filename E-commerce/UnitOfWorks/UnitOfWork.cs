using AutoMapper;
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
        IAccountRepository accountRepository;
        IReviewRepository reviewRepository;
        ICartRepository cartRepository;

        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;



        public UnitOfWork(EcommerceContext _db,Cloudinary _cloudinary,IMapper _mapper)
        {
            db = _db;
            cloudinary = _cloudinary;
            mapper = _mapper;
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

        public IAccountRepository AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(db);
                }

                return accountRepository;
            }

        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (reviewRepository == null)
                {
                    reviewRepository = new ReviewRepository(db,mapper);
                }

                return reviewRepository;
            }

        }

        public ICartRepository CartRepository
        {
            get
            {
                if (cartRepository == null)
                {
                    cartRepository = new CartRepository(db);
                }

                return cartRepository;
            }

        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
