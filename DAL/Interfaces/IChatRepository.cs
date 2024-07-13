using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetAvailableChatRoomsAsync(Guid userId);
    }
}
