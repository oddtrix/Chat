using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity(
                "UserChat",
                l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(User.Id)),
                r => r.HasOne(typeof(Chat)).WithMany().HasForeignKey("ChatsId").HasPrincipalKey(nameof(Chat.Id)),
                j => j.HasKey("ChatsId", "UsersId"));

            builder.HasMany(u => u.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);
        }
    }
}
