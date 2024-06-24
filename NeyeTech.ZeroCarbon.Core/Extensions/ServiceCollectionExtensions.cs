using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NeyeTech.ZeroCarbon.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyResolvers(this IServiceCollection services, IConfiguration configuration, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services, configuration);
            }
        }
    }
}
