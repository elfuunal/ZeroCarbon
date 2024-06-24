using Autofac;
using Autofac.Extensions.DependencyInjection;
using NeyeTech.ZeroCarbon.Business.DependencyResolvers;
using NeyeTech.ZeroCarbon.Business.Helpers;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using NeyeTech.ZeroCarbon.Api.Infrastructure;
using NeyeTech.ZeroCarbon.Core.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

//Custom Services
builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddCustomCacheServices();

builder.Services.AddZeroCarbonDbContext();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new AutofacBusinessModule()));

var app = builder.Build();

// VERY IMPORTANT. Since we removed the build from AddDependencyResolvers, let's set the Service provider manually.
// By the way, we can construct with DI by taking type to avoid calling static methods in aspects.
ServiceTool.ServiceProvider = app.Services;

app.ConfigureCustomExceptionMiddleware();

await app.UseDbOperationClaimCreator();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("v1/swagger.json", "Zero Carbon App");
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (httpContext, next) =>
{
    LogContext.PushProperty("Username", Utils.Username);
    LogContext.PushProperty("ClientIp", httpContext.Connection.RemoteIpAddress);

    await next.Invoke();
});

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
