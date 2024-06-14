using E_commerce.Models;

namespace E_commerce.Repository
{
    public class AccountRepository : Repository<ApplicationUser>,IAccountRepository
    {

        public AccountRepository(EcommerceContext db) : base(db)
        {

        }

    }
}
