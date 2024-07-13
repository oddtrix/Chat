using DAL.DTOs;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> DeleteUserAsync(Guid userId);

        Task<User> UserExistsAsync(Guid userId);

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> CreateUserAsync(string username);

        Task<User> ChangeUserNameAsync(ChangeUserNameDTO changeUserNameDTO);
    }
}
