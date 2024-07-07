using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class SettingsConfiguration : IEntityTypeConfiguration<SettingsModel>
    {
        public void Configure(EntityTypeBuilder<SettingsModel> builder)
        {
            builder.ToTable("settings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(p => p.UserId).HasColumnName("USER_ID").IsRequired();
            builder.Property(p => p.IsVisibleStatus).HasColumnName("IS_VISIBLE_STATUS").IsRequired();
            builder.Property(p => p.IsVisibleLastSeen).HasColumnName("IS_VISIBLE_LAST_SEEN").IsRequired();
            builder.Property(p => p.IsVisibleMessageSeen).HasColumnName("IS_VISIBLE_MESSAGE_SEEN");

            builder.HasOne(p => p.User)
                .WithOne(p => p.Settings)
                .HasForeignKey<SettingsModel>(p => p.UserId)
                .HasConstraintName("FK_Settings_User")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
