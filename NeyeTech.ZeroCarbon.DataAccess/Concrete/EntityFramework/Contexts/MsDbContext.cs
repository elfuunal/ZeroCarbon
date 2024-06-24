using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class MsDbContext : ProjectDbContext
    {
        IHttpContextAccessor _httpContextAccessor;

        public const string DEFAULT_SCHEMA = "dbo";

        public MsDbContext(DbContextOptions<MsDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(options, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ZeroCarbonContext")));
            }
        }
    }
}
