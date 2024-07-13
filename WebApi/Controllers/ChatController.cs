using BLL.Interfaces;
using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using DAL.Contexts;
using WebApi.Hubs;

namespace WebApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController( IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableChatRooms(Guid userId) 
        {
            var chatRooms = await this.chatService.GetAvailableChatRoomsAsync(userId);
            return Ok(chatRooms);
        }
    }
}
