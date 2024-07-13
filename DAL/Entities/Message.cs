namespace DAL.Entities
{
    public class Message : Base
    {
        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public Guid SenderId { get; set; }

        public User Sender { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
