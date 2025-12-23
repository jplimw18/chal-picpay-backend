using PicpayChal.App.Data;
using PicpayChal.App.Models;
using PicpayChal.App.Repositories.Interfaces;

namespace PicpayChal.App.Repositories
{
    public class WalletRepository(AppDbContext context) 
        : IBaseRepository<Wallet>
    {
        private readonly AppDbContext _context = context;

        public Wallet Create(Wallet entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Wallet entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Wallet>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Wallet> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Wallet Update(Wallet entity)
        {
            throw new NotImplementedException();
        }
    }
}