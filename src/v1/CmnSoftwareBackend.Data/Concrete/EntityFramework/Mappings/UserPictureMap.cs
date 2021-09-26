using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class UserPictureMap:IEntityTypeConfiguration<UserPicture>
    {
      
        public void Configure(EntityTypeBuilder<UserPicture> builder)
        {
            builder.HasKey(userPicture => userPicture.Id);
            builder.Property(userPicture => userPicture.Id).ValueGeneratedOnAdd();
            //User
            builder.HasOne<User>(userPicture => userPicture.User).WithOne(user => user.UserPicture)
                .HasForeignKey<UserPicture>(up => up.UserId);
            builder.Property(userPicture => userPicture.File).IsRequired();
            builder.Property(userPicture => userPicture.File).HasColumnType("VARBINARY(MAX)");

            builder.Property(userPicture => userPicture.IsDeleted).IsRequired();
            builder.Property(userPicture => userPicture.IsActive).IsRequired();
            builder.Property(userPicture => userPicture.CreatedDate).IsRequired(false);
            builder.Property(userPicture => userPicture.CreatedByUserId).IsRequired(false);
            builder.Property(userPicture => userPicture.ModifiedDate).IsRequired(false);
            builder.Property(userPicture => userPicture.ModifiedByUserId).IsRequired(false);

            builder.ToTable("UserPictures");
        }
    }
}
