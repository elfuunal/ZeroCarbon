using NeyeTech.ZeroCarbon.Core.Entities;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<EmissionSourceScope> EmissionSourceScopes { get; set; }
        public DbSet<EmissionSource> EmissionSources { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyData> CompanyDatas { get; set; }
        public DbSet<CompanyEmissionSource> CompanyEmissionSources { get; set; }
        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ZeroCarbonContext"))
                    .EnableSensitiveDataLogging());
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.RecordDate = DateTime.Now;
                                entityReference.RecordUsername = Utils.Username;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.RecordDate).IsModified = false;
                                Entry(entityReference).Property(x => x.RecordUsername).IsModified = false;

                                entityReference.UpdateDate = DateTime.Now;
                                entityReference.UpdateUsername = Utils.Username;
                                break;
                            }
                    }

                    entityReference.Ip = Utils.ClientIp;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
