using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class CommentWithUserMap : IEntityTypeConfiguration<CommentWithUser>
    {


        public void Configure(EntityTypeBuilder<CommentWithUser> builder)
        {
            builder.HasKey(cwu => cwu.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.HasOne<Article>(cwu => cwu.Article).WithMany(a => a.CommentWithUsers).HasForeignKey(cwu => cwu.ArticleId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne<User>(cwu => cwu.User).WithMany(a => a.CommentWithUsers).HasForeignKey(cwu => cwu.UserId);
            builder.ToTable("CommentWithUsers");
        }
    }
}