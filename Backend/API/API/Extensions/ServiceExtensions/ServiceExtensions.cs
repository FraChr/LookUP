using API.Model;
using API.Services;
using API.Services.Interfaces;
using API.Storage;

namespace API.Extensions.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICrudService<Items>, ItemService>();
        services.AddScoped<ICrudService<Location>, LocationService>();
        services.AddScoped<EndpointMapperService>();

        return services;
    }
}