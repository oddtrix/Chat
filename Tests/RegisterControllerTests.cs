using BLL.Interfaces;
using DAL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace Tests
{
    public class RegisterControllerTests
    {
        private readonly Mock<IUserService> mockRegisterService;
        private readonly RegisterController controller;

        public RegisterControllerTests()
        {
            this.mockRegisterService = new Mock<IUserService>();
            this.controller = new RegisterController(this.mockRegisterService.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenUserNameIsNullOrEmpty()
        {
            var createUserDTO = new CreateUserDTO { UserName = "" };

            var result = await controller.CreateUser(createUserDTO);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnOk_WhenUserNameIsString()
        {
            var createUserDTO = new CreateUserDTO { UserName = "TestUser" };

            var result = await controller.CreateUser(createUserDTO);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ChangeUserName_ShouldReturnBadRequest_WhenUserNameIsNullOrEmpty()
        {
            var changeUserNameDTO = new ChangeUserNameDTO { UserId = Guid.NewGuid(), UserName = "" };

            var result = await controller.ChangeUserName(changeUserNameDTO);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ChangeUserName_ShouldReturnOk_WhenUserNameIsString()
        {
            var changeUserNameDTO = new ChangeUserNameDTO { UserId = Guid.NewGuid(), UserName = "NewName" };

            var result = await controller.ChangeUserName(changeUserNameDTO);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}