using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(Guid userId);

        Task<User> FindByName(string username);
    }
}
