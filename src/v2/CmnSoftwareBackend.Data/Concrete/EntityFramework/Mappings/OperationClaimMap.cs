using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class OperationClaimMap:IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(100);
            builder.Property(o => o.IsActive).IsRequired();
            builder.Property(o => o.IsDeleted).IsRequired();

            builder.ToTable("OperationClaims");

            builder.HasData(
                new OperationClaim
                {
                    Id = 1,
                    Name = "Admin",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByUserId = null,
                    ModifiedByUserId = null,
                    ModifiedDate = null,
                },
                new OperationClaim
                {
                    Id = 2,
                    Name = "NormalUser",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByUserId = null,
                    ModifiedByUserId = null,
                    ModifiedDate = null,
                }
            );

        }
    }
}
