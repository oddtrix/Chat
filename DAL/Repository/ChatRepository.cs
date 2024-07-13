using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Contexts;

namespace DAL.Repository
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly DomainContext context;

        public ChatRepository(DomainContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await this.context.Set<Chat>()
                .Include(c => c.Creator)
                .Include(c => c.Messages)
                .Include(c => c.Users)
                .ToListAsync();
        }

        public async Task<Chat> GetByIdAsync(Guid guid)
        {
            return await this.context.Set<Chat>()
                .Include(c => c.Creator)
                .Include(c => c.Messages)
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == guid);
        }

        public async Task<IEnumerable<Chat>> GetAvailableChatRoomsAsync(Guid userId)
        {
            return await this.context.Set<Chat>()
                .Where(c => c.CreatorId != userId)
                .ToListAsync();
        }
    }
}
