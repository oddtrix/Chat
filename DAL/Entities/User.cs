namespace DAL.Entities
{
    public class User : Base
    {
        public string Name { get; set; }

        public ICollection<Chat> Chats { get; set; } = new List<Chat>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
