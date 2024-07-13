using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService chatService;

        private readonly IUserService registerService;

        private readonly static ConnectionMapping<string> connections = new ConnectionMapping<string>();

        public ChatHub(IChatService chatService, IUserService registerService)
        {
            this.chatService = chatService;
            this.registerService = registerService;
        }

        public async Task CreateChatRoom(string userId, string chatRoomName)
        {
            try
            {
                var user = await this.CheckUserId(userId);
                this.CheckChatRoomName(chatRoomName);

                var chat = await this.chatService.CreateChatRoomAsync(Guid.Parse(userId), chatRoomName);
                await this.chatService.JoinChatRoomAsync(user, chat);
                connections.Add(chat.Id.ToString(), Context.ConnectionId);
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chat.Id.ToString());
                await this.Clients.Group(chat.Id.ToString()).SendAsync(user.Name + " has joined.");
            }
            catch (Exception ex)
            {
                await this.Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task JoinChatRoom(string userId, string chatId)
        {
            try
            {
                var user = await this.CheckUserId(userId);
                var chat = await this.CheckRoomId(chatId);

                await this.chatService.JoinChatRoomAsync(user, chat);
                connections.Add(chat.Id.ToString(), Context.ConnectionId);
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chat.Id.ToString());
                await this.Clients.Group(chat.Id.ToString()).SendAsync(user.Name + " has joined.");
            }
            catch (Exception ex)
            {
                await this.Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task SendMessage(string userId, string chatId, string message)
        {
            try
            {
                var user = await this.CheckUserId(userId);
                var chat = await this.CheckRoomId(chatId);

                var newMessage = await this.chatService.SendMessageAsync(user, chat, message);
                var messageJson = JsonConvert.SerializeObject(newMessage, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                await this.Clients.Group(chat.Id.ToString()).SendAsync(messageJson.ToString());
            }
            catch (Exception ex)
            {
                await this.Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task DeleteChatRoom(string userId, string chatId)
        {
            try
            {
                var user = await this.CheckUserId(userId);
                var chat = await this.CheckRoomId(chatId);

                await this.chatService.DeleteChatRoomAsync(user, chat);
                var usersInGroup = connections.GetConnections(chatId).ToList();

                await this.Clients.Group(chat.Id.ToString()).SendAsync("Chat room has been deleted");

                foreach (var connectionId in usersInGroup)
                {
                    await Groups.RemoveFromGroupAsync(connectionId, chat.Id.ToString());
                    connections.Remove(chatId, connectionId);
                }

            }
            catch (Exception ex)
            {
                await this.Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task LeaveChatRoom(string userId, string chatId)
        {
            try
            {
                var user = await this.CheckUserId(userId);
                var chat = await this.CheckRoomId(chatId);

                await this.chatService.LeaveChatRoomAsync(user, chat);
                var usersInGroup = connections.GetConnections(chatId).ToList();
                
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());

                await this.Clients.Group(chat.Id.ToString()).SendAsync($"{user.Name} has left.");
            }
            catch (Exception ex)
            {
                await this.Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task<User> CheckUserId(string userId)
        {
            var user = await this.registerService.UserExistsAsync(Guid.Parse(userId));
            if (user == null)
            {
                throw new Exception("User not found");
            }
              
            return user;
        }

        public async Task<Chat> CheckRoomId(string roomId)
        {
            var room = await this.chatService.ChatRoomExistsAsync(Guid.Parse(roomId));
            if (room == null)
            {
                throw new Exception("Chat room not found.");
            }

            return room;
        }

        public string CheckChatRoomName(string chatRoomName)
        {
            if (string.IsNullOrEmpty(chatRoomName))
            {
                throw new Exception("Incorrect name of the chat room");
            }

            return chatRoomName;
        }
    }
}
