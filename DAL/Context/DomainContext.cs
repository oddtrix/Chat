using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions<DomainContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Chat).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Message).Assembly);
        }
    }
}