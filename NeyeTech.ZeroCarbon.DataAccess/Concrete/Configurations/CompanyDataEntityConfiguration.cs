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
    public class CompanyDataEntityConfiguration : IEntityTypeConfiguration<CompanyData>
    {
        public void Configure(EntityTypeBuilder<CompanyData> builder)
        {
            builder.ToTable("CompanyDatas", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value).HasPrecision(18, 4);

            builder.HasOne(s => s.Company).WithMany(s => s.CompanyDatas).HasForeignKey(s => s.CompanyId);
            builder.HasOne(s => s.EmissionSource).WithMany(s => s.CompanyDatas).HasForeignKey(s => s.EmissionSourceId);
        }
    }
}
