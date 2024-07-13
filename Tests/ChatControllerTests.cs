using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace Tests
{
    public class ChatControllerTests
    {
        private readonly Mock<IChatService> chatServiceMock;
        private readonly ChatController controller;

        public ChatControllerTests()
        {
            this.chatServiceMock = new Mock<IChatService>();
            this.controller = new ChatController(this.chatServiceMock.Object);
        }

        [Fact]
        public async Task GetAvailableChatRooms_ShouldReturnOk_WhenUserIdIsGuid()
        {
            var userId = Guid.NewGuid();

            var result = await controller.GetAvailableChatRooms(userId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
