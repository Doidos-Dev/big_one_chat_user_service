
using UserService.Test.Domain.Entities.Base;
using UserService.Test.Domain.Enums;

namespace UserService.Test.Domain.Entities
{
    public class UserModel : BaseEntity
    {
        public string? Name { get; private set; }
        public string? Nickname { get; private set; }
        public StatusEnum Status { get; private set; }
        public string? Photo { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public DateTime? LastSeen { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public SettingsModel? Settings { get; set; }

        public UserModel(Guid id,
           string name,
           string nickname,
           StatusEnum status,
           string photo,
           string email,
           string password,
           DateTime? lastSeen,
           DateTime createdDate)
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

        public UserModel(Guid id,
            string name,
            string nickname,
            string photo,
            string email,
            string password)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Photo = photo;
            Email = email;
            Password = password;
        }

        public UserModel(
            Guid id,
            string name,
            string nickname,
            string photo,
            StatusEnum status)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Photo = photo;
            Status = status;
        }

        public void EncryptPasswordEntity(string passwordEncrypted)
        {
            Password = passwordEncrypted;
        }

        public void ChangeStatus()
        {
            if (Status == StatusEnum.Online)
                Status = StatusEnum.Offline;
            else
                Status = StatusEnum.Online;
        }
    }
}
