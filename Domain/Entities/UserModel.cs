using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserModel : BaseEntity
    {
        public string? Name { get; private set; }
        public string? Nickname { get; private set; }
        public StatusEnum Status { get; private set; }
        public string? PhotoUrl { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public DateTimeOffset? LastSeen { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public SettingsModel? Settings { get; set; }

        public UserModel(Guid id,
            string name,
            string nickname,
            StatusEnum status,
            string photoUrl,
            string email,
            string password,
            DateTimeOffset? lastSeen,
            DateTimeOffset createdDate)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Status = status;
            PhotoUrl = photoUrl;
            Email = email;
            Password = password;
            LastSeen = lastSeen;
            CreatedDate = createdDate;
            Settings = new SettingsModel(
                Guid.NewGuid(), Id, true, true, true
                );
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
            PhotoUrl = photo;
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
            PhotoUrl = photo;
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

        public void ChangePhoto(string newPhoto)
        {
            PhotoUrl = newPhoto;
        }
    }
}