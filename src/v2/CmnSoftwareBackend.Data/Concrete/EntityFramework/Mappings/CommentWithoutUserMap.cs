using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class CommentWithoutUserMap:IEntityTypeConfiguration<CommentWithoutUser>
    {
       
        public void Configure(EntityTypeBuilder<CommentWithoutUser> builder)
        {
            builder.HasKey(cwu => cwu.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(cwu => cwu.Text).IsRequired();
            builder.Property(cwu => cwu.Text).HasMaxLength(150);
            builder.Property(cwu => cwu.Star).IsRequired();
            builder.Property(cwu => cwu.UserName).IsRequired();
            builder.Property(cwu => cwu.UserName).HasMaxLength(30);


            builder.HasOne<Article>(c => c.Article).WithMany(a=> a.CommentWithoutUsers).HasForeignKey(cwu=>cwu.ArticleId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
            builder.ToTable("CommentWithoutUsers");
        }
    }
}
