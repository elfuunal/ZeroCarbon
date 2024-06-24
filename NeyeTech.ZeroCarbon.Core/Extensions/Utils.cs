using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NeyeTech.ZeroCarbon.Core.Extensions
{
    public static class Utils
    {
        public static long UserId 
        {
            get 
            {
                var context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    return Convert.ToInt64(context.HttpContext.User.Claims.First(s => s.Type == "UserId").Value);
                }

                return 0;
            }
            private set { } 
        }

        public static string Username
        {
            get
            {
                var context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    return context.HttpContext.User.Claims.First(s => s.Type == "UserCode").Value;
                }

                return "-";
            }
            private set { }
        }

        public static string ClientIp
        {
            get
            {
                return "0.0.0.0";
            }
            private set { }
        }

        public static string ToCoolString(this decimal value)
        {
            return value.ToString(value % 1 == 0 ? "N0" : "N3"); // Or F0/F2 ;)
        }

        public static string ToCoolString2(this decimal value)
        {
            return Convert.ToInt64(value).ToString();
        }
    }
}
