using API.Model;
using API.Services;
using API.Services.Interfaces;
using API.Storage;
using Microsoft.AspNetCore.Http.Features;

namespace API.Extensions.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICrudService<Items>, ItemService>();
        services.AddScoped<ICrudService<Location>, LocationService>();
        services.AddScoped<EndpointMapperService>();
        // services.AddProblemDetails(options =>
        // {
        //     options.CustomizeProblemDetails = context =>
        //     {
        //         context.ProblemDetails.Instance =
        //             $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        //         context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
        //         var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        //         context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
        //     };
        // });

        return services;
    }
}