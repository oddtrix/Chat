using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using DAL.Contexts;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DomainContext context;

        private IUserRepository userRepository;

        private IChatRepository chatRepository;

        private IRepository<Message> messageRepository;

        public UnitOfWork(DomainContext context)
        {
            this.context = context;
        }

        public IChatRepository ChatRepository
        {
            get
            {

                if (this.chatRepository == null)
                {
                    this.chatRepository = new ChatRepository(this.context);
                }

                return chatRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context);
                }

                return userRepository;
            }
        }

        public IRepository<Message> MessageRepository
        {
            get
            {

                if (this.messageRepository == null)
                {
                    this.messageRepository = new GenericRepository<Message>(this.context);
                }

                return messageRepository;
            }
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
