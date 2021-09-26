using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class ArticlePictureMap:IEntityTypeConfiguration<ArticlePicture>
    {
        public void Configure(EntityTypeBuilder<ArticlePicture> builder)
        {
            builder.HasKey(ap => ap.Id);
            builder.Property(ap => ap.File).IsRequired();
            builder.HasOne<Article>(ap => ap.Article).WithMany(a => a.ArticlePictures).HasForeignKey(ap=>ap.ArticleId);
            builder.ToTable("ArticlePictures");
        }
    }
}
