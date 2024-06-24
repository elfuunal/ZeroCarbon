using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.Configurations
{
    public class GroupClaimEntityConfiguration : IEntityTypeConfiguration<GroupClaim>
    {
        public void Configure(EntityTypeBuilder<GroupClaim> builder)
        {
            builder.ToTable("GroupClaims", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
        }
    }
}
