using Domain.Enums;

namespace Domain.Entities
{
    public class UserModel
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Nickname { get; private set; }
        public StatusEnum Status { get; private set; }
        public string? Photo {  get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public DateTime? LastSeen { get; private set; }
        public DateTime? CreatedDate { get; private set; }

        public UserModel(Guid id,
            string name,
            string nickname,
            StatusEnum status,
            string photo,
            string email,
            string password,
            DateTime? lastSeen,
            DateTime? createdDate)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Status = status;
            Photo = photo;
            Email = email;
            Password = password;
            LastSeen = lastSeen;
            CreatedDate = createdDate;
        }

        public UserModel()
        {
            
        }
    }
}
