using DAL.Entities;
using DAL.Repository;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IChatRepository ChatRepository { get; }

        IRepository<Message> MessageRepository { get; }

        Task SaveAsync();
    }
}
