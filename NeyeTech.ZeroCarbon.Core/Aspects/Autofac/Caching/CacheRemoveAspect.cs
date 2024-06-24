using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching;
using NeyeTech.ZeroCarbon.Core.Utilities.Interceptors;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// CacheRemoveAspect
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
