namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get;protected set; }
    }
}
