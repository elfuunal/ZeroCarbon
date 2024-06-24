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
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", MsDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
        }
    }
}
