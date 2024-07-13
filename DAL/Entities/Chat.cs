namespace DAL.Entities
{
    public class Chat : Base
    {
        public string Name { get; set; }

        public Guid CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
