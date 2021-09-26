using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryAndArticleMap:IEntityTypeConfiguration<CategoryAndArticle>
    {
        
        
        public void Configure(EntityTypeBuilder<CategoryAndArticle> builder)
        {
            builder.HasKey(ca => new { ca.CateogryId, ca.ArticleId });
            //builder.Property(ca=>new {ca.CategoryId,ca.ArticleId }).ValueGeneratedOnAdd();
            builder.HasOne<Category>(ca => ca.Category).WithMany(c => c.CategoryAndArticles).HasForeignKey(ca=>ca.CateogryId);
            builder.HasOne<Article>(ca => ca.Article).WithMany(c => c.CategoryAndArticles).HasForeignKey(ca => ca.ArticleId);
            builder.ToTable("CategoryAndArticles");
        }
    }
}
