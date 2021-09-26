using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap:IEntityTypeConfiguration<Article>
    {
        

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasMaxLength(3000);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(100);
           
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
           
            //wrong configure relationship
            //builder.HasOne<Category>(a => a.Category).WithMany(u => u.Articles).HasForeignKey(a => a.CategoryId);
            //builder.HasOne<ArticlePicture>(a => a.ArticlePicture).WithMany(ap => ap.Articles).HasForeignKey(a => a.ArticlePictureId).IsRequired(true).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);

            builder.ToTable("Articles");
        }
    }
}
