using BLL.Interfaces;
using DAL.DTOs;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> ChangeUserNameAsync(ChangeUserNameDTO changeUserNameDTO)
        {
            var user = await this.UserExistsAsync(changeUserNameDTO.UserId);
            user.Name = changeUserNameDTO.UserName;
            this.unitOfWork.UserRepository.Update(user);
            await this.unitOfWork.SaveAsync();

            return user;
        }

        public async Task<User> CreateUserAsync(string username)
        {
            var user = await this.unitOfWork.UserRepository.FindByName(username);
            if (user == null)
            {
                user = new User { Name = username };
                await this.unitOfWork.UserRepository.CreateAsync(user);
                await this.unitOfWork.SaveAsync();
            }

            return user;
        }

        public async Task<User> DeleteUserAsync(Guid userId)
        {
            var user = await this.UserExistsAsync(userId);
            this.unitOfWork.UserRepository.Delete(user);
            await this.unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await this.unitOfWork.UserRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await this.unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> UserExistsAsync(Guid userId)
        {
            var user = await this.unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Wrong userId.");
            }

            return user;
        }
    }
}
