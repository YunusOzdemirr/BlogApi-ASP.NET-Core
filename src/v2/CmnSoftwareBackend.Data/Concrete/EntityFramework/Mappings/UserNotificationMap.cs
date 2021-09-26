using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class UserNotificationMap:IEntityTypeConfiguration<UserNotification>
    {

        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Message).IsRequired();
            builder.Property(u => u.Message).HasMaxLength(200);
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();

            builder.HasOne<User>(un => un.User).WithMany(u => u.UserNotifications).HasForeignKey(un => un.UserId);

            builder.ToTable("UserNotifications");
        }
    }
}
