using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_commerce.Models;
using System.Net;
using Task.Repository;

namespace E_commerce.Repository
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {

        public CategoryRepository(EcommerceContext db) : base(db) 
        {
           
        }
    }
}
