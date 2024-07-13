namespace DAL.DTOs
{
    public class ChangeUserNameDTO : CreateUserDTO
    {
        public Guid UserId { get; set; }
    }
}
