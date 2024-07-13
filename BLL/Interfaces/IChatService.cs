using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IChatService
    {
        Task<Chat> ChatRoomExistsAsync(Guid roomId);

        Task JoinChatRoomAsync(User user, Chat chat);

        Task LeaveChatRoomAsync(User user, Chat chat);

        Task DeleteChatRoomAsync(User user, Chat chat);

        Task<Chat> CreateChatRoomAsync(Guid userId, string chatName);

        Task<IEnumerable<Chat>> GetAvailableChatRoomsAsync(Guid userId);

        Task<Message> SendMessageAsync(User user, Chat chat, string message);
    }
}
