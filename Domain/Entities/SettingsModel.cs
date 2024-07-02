using Domain.Entities.Base;

namespace Domain.Entities
{
    public class SettingsModel : BaseEntity
    {
        public Guid UserId { get; private set; }
        public bool IsVisibleStatus { get; private set; }
        public bool IsVisibleLastSeen { get; private set; }
        public bool IsVisibleMessageSeen { get; private set; }
        public UserModel? User { get; set; }

        public SettingsModel(Guid id,
            Guid userId,
            bool isVisibleStatus,
            bool isVisibleLastSeen,
            bool isVisibleMessageSeen)
        {
            Id = id;
            UserId = userId;
            IsVisibleStatus = isVisibleStatus;
            IsVisibleLastSeen = isVisibleLastSeen;
            IsVisibleMessageSeen = isVisibleMessageSeen;
        }

        public SettingsModel()
        {
            
        }
    }
}
