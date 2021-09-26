using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryMap:IEntityTypeConfiguration<Category>
    {
       
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Image).IsRequired(false);
            builder.Property(s => s.Image).HasMaxLength(250);
            builder.Property(c => c.Description).IsRequired(false);
            builder.Property(c => c.Description).HasMaxLength(200);

            builder.Property(c => c.Color).IsRequired();
            builder.Property(c => c.Color).HasMaxLength(20);

            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            //wrong configure mapping
            //builder.HasOne<Article>(c => c.Article).WithMany(a => a.Categories).HasForeignKey(c => c.ArticleId);
            builder.HasOne<Rank>(c => c.Rank).WithMany(r => r.Categories).HasForeignKey(c => c.RankId);
            builder.ToTable("Categories");
        }
    }
}
