using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.Configurations
{
    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
        }
    }
}
