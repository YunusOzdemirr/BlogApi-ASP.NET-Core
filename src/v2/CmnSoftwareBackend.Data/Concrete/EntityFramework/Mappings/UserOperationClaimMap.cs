using System;
using CmnSoftwareBackend.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmnSoftwareBackend.Data.Concrete.EntityFramework.Mappings
{
    public class UserOperationClaimMap:IEntityTypeConfiguration<UserOperationClaim>
    {
       
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(uo => new { uo.UserId, uo.OperationClaimId });
            builder.HasOne<User>(uo => uo.User).WithMany(u => u.UserOperationClaims).HasForeignKey(uo => uo.UserId);
            builder.HasOne<OperationClaim>(uo => uo.OperationClaim).WithMany(oc => oc.UserOperationClaims)
                .HasForeignKey(uo => uo.OperationClaimId);

            builder.ToTable("UserOperationClaims");
        }
    }
}
