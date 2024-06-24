using NeyeTech.ZeroCarbon.Business.BusinessAspects;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace NeyeTech.ZeroCarbon.Business.Helpers
{
    public static class OperationClaimCreatorMiddleware
    {
        public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app)
        {
            //var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
            //foreach (var operationName in GetOperationNames())
            //{
            //    await mediator.Send(new CreateOperationClaimInternalCommand
            //    {
            //        ClaimName = operationName
            //    });
            //}

            //var operationClaims = (await mediator.Send(new GetOperationClaimsInternalQuery())).Data;
            //var user = await mediator.Send(new RegisterUserInternalCommand
            //{
            //    FullName = "System Admin",
            //    Password = "Q1w212*_*",
            //    Email = "admin@adminmail.com",
            //});
            //await mediator.Send(new CreateUserClaimsInternalCommand
            //{
            //    UserId = 1,
            //    OperationClaims = operationClaims
            //});
        }

        private static IEnumerable<string> GetOperationNames()
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    // runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
                    x.Namespace != null && x.Namespace.StartsWith("NeyeTech.ZeroCarbon.Business") &&
                    (x.Name.EndsWith("Command") || x.Name.EndsWith("Query")) &&
                    x.CustomAttributes.All(a => a.AttributeType == typeof(SecuredOperation)))
                .Select(x => x.Name);
            return assemblyNames;
        }
    }
}
