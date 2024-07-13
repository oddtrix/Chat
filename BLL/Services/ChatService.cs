using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserService registerService;

        public ChatService(IUnitOfWork unitOfWork, IUserService registerService)
        {
            this.unitOfWork = unitOfWork;
            this.registerService = registerService;
        }

        public async Task<Chat> CreateChatRoomAsync(Guid userId, string chatName)
        {
            var chat = new Chat
            {
                Name = chatName,
                CreatorId = userId
            };

            await this.unitOfWork.ChatRepository.CreateAsync(chat);
            await this.unitOfWork.SaveAsync();

            return chat;
        }

        public async Task<Chat> ChatRoomExistsAsync(Guid roomId)
        {
            var chat = await this.unitOfWork.ChatRepository.GetByIdAsync(roomId);
            if (chat == null)
            {
                throw new Exception("Wrong roomId");
            }

            return chat;
        }

        public async Task JoinChatRoomAsync(User user, Chat chat)
        {            
            chat.Users.Add(user);            
            await this.unitOfWork.SaveAsync();
        }

        public async Task<Message> SendMessageAsync(User user, Chat chat, string message)
        {
            var newMessage = new Message
            {
                Chat = chat,
                ChatId = chat.Id,
                Content = message,
                DateTime = DateTime.Now,
                SenderId = user.Id,
                Sender = user
            };

            await this.unitOfWork.MessageRepository.CreateAsync(newMessage);
            await this.unitOfWork.SaveAsync();

            return newMessage;  
        }

        public async Task DeleteChatRoomAsync(User user, Chat chat)
        {
            if (chat.CreatorId != user.Id)
            {
                throw new Exception("There are no permissions to do the operation");
            }
            this.unitOfWork.ChatRepository.Delete(chat);
            await this.unitOfWork.SaveAsync();
        }

        public async Task LeaveChatRoomAsync(User user, Chat chat)
        {
            chat.Users.Remove(user);
            this.unitOfWork.ChatRepository.Update(chat);
            await this.unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Chat>> GetAvailableChatRoomsAsync(Guid userId)
        {
            return await this.unitOfWork.ChatRepository.GetAvailableChatRoomsAsync(userId);
        }
    }
}
