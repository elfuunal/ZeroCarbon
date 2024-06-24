using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.Configurations
{
    public class CountyEntityConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.ToTable("Counties", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.City).WithMany(s => s.Counties).HasForeignKey(s => s.CityId);
        }
    }
}
