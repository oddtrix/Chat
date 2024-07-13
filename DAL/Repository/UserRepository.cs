using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Contexts;

namespace DAL.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DomainContext context;

        public UserRepository(DomainContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await this.context.Users
                .Include(u => u.Messages)
                .Include(u => u.Chats)
                .FirstOrDefaultAsync(e => e.Id == userId);
        }

        public async Task<User> FindByName(string username)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Name == username);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await this.context.Set<User>().Include(u => u.Chats).ToListAsync();
        }
    }
}
