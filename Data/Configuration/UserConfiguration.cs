using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Name).HasColumnName("NAME").HasColumnType("TEXT").IsRequired();
            builder.Property(x => x.Nickname).HasColumnName("NICK_NAME").HasColumnType("TEXT").IsRequired();
            builder.Property(x => x.Status).HasColumnName("STATUS").IsRequired();
            builder.Property(x => x.PhotoUrl).HasColumnName("PHOTO_URL").HasColumnType("TEXT").IsRequired();
            builder.Property(x => x.Email).HasColumnName("EMAIL").HasColumnType("TEXT").IsRequired();
            builder.Property(x => x.Password).HasColumnName("PASSWORD").HasColumnType("TEXT").IsRequired();
            builder.Property(x => x.LastSeen).HasColumnName("LAST_SEEN");
            builder.Property(x => x.CreatedDate).HasColumnName("CREATED_DATE");
        }
    }
}
