using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.Configurations
{
    public class CompanyEmissionSourceEntityConfiguration : IEntityTypeConfiguration<CompanyEmissionSource>
    {
        public void Configure(EntityTypeBuilder<CompanyEmissionSource> builder)
        {
            builder.ToTable("CompanyEmissionSources", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.EmissionSource).WithMany(s => s.CompanyEmissionSources).HasForeignKey(s => s.EmissionSourceId);
            builder.HasOne(s => s.Company).WithMany(s => s.CompanyEmissionSources).HasForeignKey(s => s.CompanyId);
        }
    }
}
