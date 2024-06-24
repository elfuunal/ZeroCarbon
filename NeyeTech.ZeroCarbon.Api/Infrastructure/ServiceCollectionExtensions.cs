using NeyeTech.ZeroCarbon.Business.DependencyResolvers;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching.Redis;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using NeyeTech.ZeroCarbon.Core.DependencyResolvers;
using NeyeTech.ZeroCarbon.Core.Extensions;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Encyption;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Jwt;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NeyeTech.ZeroCarbon.Core.Utilities.Settings;

namespace NeyeTech.ZeroCarbon.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddTransient<ITokenHelper, JwtHelper>();
        }

        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AutofacBusinessModule));

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowOrigin",
                    builder =>
                    builder
                    .WithOrigins("http://10.0.2.16", "https://10.0.2.16", "10.0.2.16")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSwaggerGen();

            services.AddTransient<MsSqlLogger>();

            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
                sp.GetService<IHttpContextAccessor>().HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(""));

            services.AddScoped<IPrincipal>(getPrincipal);

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(configuration, new ICoreModule[] { coreModule });

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
            };
        }

        public static void AddCustomCacheServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, RedisCacheManager>();
        }

        public static void AddZeroCarbonDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext, MsDbContext>();
        }
    }
}
