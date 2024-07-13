using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfiguration
{
    public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Creator)
               .WithMany()
               .HasForeignKey(c => c.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Users)
                .WithMany(u => u.Chats)
                .UsingEntity(
                "UserChat",
                l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(User.Id)),
                r => r.HasOne(typeof(Chat)).WithMany().HasForeignKey("ChatsId").HasPrincipalKey(nameof(Chat.Id)),
                j => j.HasKey("ChatsId", "UsersId"));

            builder.HasMany(u => u.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);
        }
    }
}
