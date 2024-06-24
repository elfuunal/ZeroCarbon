using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.Configurations
{
    public class OperationClaimEntityConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
        }
    }
}
