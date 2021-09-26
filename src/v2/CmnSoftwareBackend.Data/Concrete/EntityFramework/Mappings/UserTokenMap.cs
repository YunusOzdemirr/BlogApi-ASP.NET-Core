using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class UserTokenMap:IEntityTypeConfiguration<UserToken>
    {

        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(ut => ut.Id);
            builder.Property(ut => ut.Id).ValueGeneratedOnAdd();

            builder.Property(ut => ut.Token).IsRequired();
            builder.Property(ut => ut.Token).HasColumnType("VARCHAR(1000)");
            builder.Property(ut => ut.TokenExpiration).IsRequired();

            builder.Property(ut => ut.RefreshToken).IsRequired();
            builder.Property(ut => ut.RefreshToken).HasColumnType("VARCHAR(1000)");
            builder.Property(ut => ut.RefreshTokenExpiration).IsRequired();

            //Nullable Fields
            builder.Property(ut => ut.CreatedByUserId).IsRequired(false);
            builder.Property(ut => ut.CreatedDate).IsRequired(false);
            builder.Property(ut => ut.ModifiedByUserId).IsRequired(false);
            builder.Property(ut => ut.ModifiedDate).IsRequired(false);
            builder.Property(ut => ut.IpAddress).IsRequired(false);
            builder.Property(ut => ut.IpAddress).HasColumnType("VARCHAR(35)");

            builder.HasOne<User>(ut => ut.User).WithMany(u => u.UserTokens).HasForeignKey(ut => ut.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("UserTokens");
        }
    }
}
