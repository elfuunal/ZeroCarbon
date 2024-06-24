using NeyeTech.ZeroCarbon.Core.ApiDoc;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching.Redis;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using NeyeTech.ZeroCarbon.Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NeyeTech.ZeroCarbon.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerMessages.Version, new OpenApiInfo
                {
                    Version = SwaggerMessages.Version,
                    Title = SwaggerMessages.Title,
                    Description = SwaggerMessages.Description
                    // TermsOfService = new Uri(SwaggerMessages.TermsOfService),
                    // Contact = new OpenApiContact
                    // {
                    //    Name = SwaggerMessages.ContactName,
                    // },
                    // License = new OpenApiLicense
                    // {
                    //    Name = SwaggerMessages.LicenceName,
                    // },
                });

                c.OperationFilter<AddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "`Token only!!!` - without `Bearer_` prefix",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });
        }
    }
}
